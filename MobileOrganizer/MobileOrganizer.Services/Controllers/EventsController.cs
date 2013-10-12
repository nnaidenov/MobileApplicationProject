using MobileOrganizer.Models;
using MobileOrganizer.Services.Attributes;
using MobileOrganizer.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ValueProviders;

namespace MobileOrganizer.Services.Controllers
{
    public class EventsController : BaseApiController
    {
        [HttpPost]
        [ActionName("create")]
        public HttpResponseMessage PostPost(CreateEventModel model,
             [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        {
            var responseMsg = ExecuteOperationOrHandleExceptions(
            () =>
            {
                var user = this.Data.Users.FirstOrDefault(u => u.SessionKey == sessionKey);
                if (user == null)
                {
                    throw new InvalidOperationException("Invalid username or password");
                }

                var newEvent = new Event
                {
                    EndDate = model.EndDate,
                    StartDate = model.StartDate,
                    Description = model.Description,
                    Owner = user,
                    OwnerId = user.Id,
                    Title = model.Title
                };

                this.Data.Events.Add(newEvent);
                this.Data.SaveChanges();

                var response = this.Request.CreateResponse(HttpStatusCode.Created);

                return response;
            });

            return responseMsg;
        }
    }
}
