using Organizer.API.Attributes;
using Organizer.API.Models;
using Organizer.API.Persisters;
using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ValueProviders;

namespace Organizer.API.Controllers
{
    public class UsersController : BaseController
    {
        [HttpPost("users/register")]
        public HttpResponseMessage RegisterUser(UserRegisterModel model)
        {
            var responseMsg = this.ExceptionHandler(
                 () =>
                 {
                     UserDataPersister.ValidateUsername(model.Username);
                     UserDataPersister.ValidateAuthCode(model.AuthCode);
                     //TODO: Validate Email
                     //UserDataPersister.ValidateEmail(model.Email);

                     var usernameToLower = model.Username.ToLower();

                     var users = this.Data.Users.All();
                     var user = users.FirstOrDefault(
                           usr => usr.Username == usernameToLower || usr.Email == model.Email);

                     if (user != null)
                     {
                         throw new InvalidOperationException("Invalid Username or Password");
                     }

                     var newUser = new User
                     {
                         Username = usernameToLower,
                         Email = model.Email,
                         AuthCode = model.AuthCode
                     };

                     var userInDb = this.Data.Users.Add(newUser);
                     var toUser = userInDb as User;
                     var sessionKey = UserDataPersister.GenerateSessionKey(toUser.Id);
                     toUser.SessionKey = sessionKey;

                     var loggedModel = new UserLoggedModel
                     {
                         Username = toUser.Username,
                         SessionKey = toUser.SessionKey
                     };

                     var response = this.Request.CreateResponse(HttpStatusCode.Created, loggedModel);
                     response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = toUser.Id }));

                     return response;
                 });

            return responseMsg;
        }

        [HttpPost("users/login")]
        public HttpResponseMessage LoginUser(UserLoginModel model)
        {
            var responseMsg = this.ExceptionHandler(
                 () =>
                 {
                     UserDataPersister.ValidateUsername(model.Username);
                     UserDataPersister.ValidateAuthCode(model.AuthCode);

                     var usernameToLower = model.Username.ToLower();

                     var users = this.Data.Users.All();
                     var user = users.FirstOrDefault(
                           usr => usr.Username == usernameToLower && usr.AuthCode == model.AuthCode);

                     if (user == null)
                     {
                         throw new InvalidOperationException("Invalid Username or Password");
                     }

                     this.Data.Users.Update(user);

                     var loggedModel = new UserLoggedModel
                     {
                         Username = user.Username,
                         SessionKey = user.SessionKey
                     };

                     var response = this.Request.CreateResponse(HttpStatusCode.Created, loggedModel);
                     response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.Id }));

                     return response;
                 });

            return responseMsg;
        }

        [HttpPut("users/logout")]
        public HttpResponseMessage LogoutUser(
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        {
            var responseMsg = this.ExceptionHandler(
                 () =>
                 {
                     var users = this.Data.Users.All();
                     var user = users.FirstOrDefault(
                           usr => usr.SessionKey == sessionKey);

                     if (user == null)
                     {
                         throw new InvalidOperationException("Invalid Username or Password");
                     }

                     this.Data.Users.Update(user);

                     var response = this.Request.CreateResponse(HttpStatusCode.OK);

                     return response;
                 });

            return responseMsg;
        }
    }
}
