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

        [HttpGet]
        [ActionName("all")]
        public ICollection<StuffListModel> All(
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

                var events = this.Data.Events.Where(t => t.OwnerId == user.Id && t.StartDate >= DateTime.Now).OrderBy(t => t.Priority);

                var modelsEvents =
                    (from e in events
                     select new StuffListModel
                     {
                         Id = e.Id,
                         Title = e.Title,
                         Type = "event"
                     });

                return modelsEvents.ToList();
            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("getById")]
        public EventModel GetById(int id,
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

                var singleEvent = this.Data.Events.Find(id);

                var model = new EventModel
                {
                    Id = singleEvent.Id,
                    Title = singleEvent.Title,
                    Description = singleEvent.Description,
                    StartDate = singleEvent.StartDate,
                    EnddDate = singleEvent.EndDate
                };

                return model;
            });

            return responseMsg;
        }

        [HttpPost]
        [ActionName("delete")]
        public bool DeleteById(int id,
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

                var eventM = this.Data.Events.Find(id);
                this.Data.Events.Remove(eventM);

                return true;
            });

            return responseMsg;
        }
    }
}
