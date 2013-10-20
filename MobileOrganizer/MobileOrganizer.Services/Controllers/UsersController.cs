using MobileOrganizer.Models;
using MobileOrganizer.Services.Attributes;
using MobileOrganizer.Services.Models;
using MobileOrganizer.Services.Persisters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ValueProviders;

namespace MobileOrganizer.Services.Controllers
{
    public class UsersController : BaseApiController
    {
        [HttpPost]
        [ActionName("register")]
        public HttpResponseMessage RegisterUser(UserRegisterModel model)
        {
            var responseMsg = this.ExecuteOperationOrHandleExceptions(
                 () =>
                 {
                     UserDataPersister.ValidateAuthCode(model.AuthCode);
                     //TODO: Validate Email
                     //UserDataPersister.ValidateEmail(model.Email);

                     var users = this.Data.Users;
                     var user = users.FirstOrDefault(
                           usr => usr.Email == model.Email);

                     if (user != null)
                     {
                         throw new InvalidOperationException("Wrong email.");
                     }

                     var newUser = new User
                     {
                         Email = model.Email,
                         AuthCode = model.AuthCode
                     };

                     var userInDb = this.Data.Users.Add(newUser);
                     this.Data.SaveChanges();

                     var sessionKey = UserDataPersister.GenerateSessionKey(userInDb.Id);
                     userInDb.SessionKey = sessionKey;
                     this.Data.SaveChanges();
                     var loggedModel = new UserLoggedModel
                     {
                         Email = userInDb.Email,
                         SessionKey = userInDb.SessionKey
                     };

                     var response = this.Request.CreateResponse(HttpStatusCode.Created, loggedModel);
                     response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = userInDb.Id }));

                     return response;
                 });

            return responseMsg;
        }

        [HttpPost]
        [ActionName("login")]
        public HttpResponseMessage LoginUser(UserLoginModel model)
        {
            var responseMsg = this.ExecuteOperationOrHandleExceptions(
                 () =>
                 {
                     //UserDataPersister.ValidateUsername(model.Username);
                     UserDataPersister.ValidateAuthCode(model.AuthCode);

                     var users = this.Data.Users;
                     var user = users.FirstOrDefault(
                           usr => usr.Email == model.Email && usr.AuthCode == model.AuthCode);

                     if (user == null)
                     {
                         throw new InvalidOperationException("Invalid Username or Password");
                     }

                     user.SessionKey = UserDataPersister.GenerateSessionKey(user.Id);
                     this.Data.SaveChanges();

                     var loggedModel = new UserLoggedModel
                     {
                         Email = user.Email,
                         SessionKey = user.SessionKey
                     };

                     var response = this.Request.CreateResponse(HttpStatusCode.Created, loggedModel);
                     response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.Id }));

                     return response;
                 });

            return responseMsg;
        }
        [HttpPut]
        [ActionName("logout")]
        public HttpResponseMessage LogoutUser(
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        {
            var responseMsg = this.ExecuteOperationOrHandleExceptions(
                 () =>
                 {
                     var users = this.Data.Users;
                     var user = users.FirstOrDefault(
                           usr => usr.SessionKey == sessionKey);

                     if (user == null)
                     {
                         throw new InvalidOperationException("Invalid Username or Password");
                     }

                     user.SessionKey = null;
                     this.Data.SaveChanges();

                     var response = this.Request.CreateResponse(HttpStatusCode.OK);

                     return response;
                 });

            return responseMsg;
        }
    }
}