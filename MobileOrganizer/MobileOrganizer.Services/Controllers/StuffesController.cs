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
    public class StuffesController : BaseApiController
    {
        [HttpGet]
        [ActionName("all")]
        public ICollection<StuffModel> All(
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

                var todoes = this.Data.Todos.Where(t => t.OwnerId == user.Id && t.Date >= DateTime.Now).OrderBy(t => t.Date);
                var events = this.Data.Events.Where(t => t.OwnerId == user.Id && t.StartDate >= DateTime.Now).OrderBy(t => t.StartDate);

                var modelsTodo =
                    (from t in todoes
                     select new StuffModel
                     {
                         Day = t.Date.Day,
                         Mount = t.Date.Month - DateTime.Now.Month,
                         Year = t.Date.Year - DateTime.Now.Year,
                         Type = "todo"
                     });

                var modelsEvents =
                    (from e in events
                     select new StuffModel
                     {
                         Day = e.StartDate.Day,
                         Mount = e.StartDate.Month - DateTime.Now.Month,
                         Year = e.StartDate.Year - DateTime.Now.Year,
                         Type = "event"
                     });

                var stuffes = new List<StuffModel>();

                foreach (var todo in modelsTodo)
                {
                    stuffes.Add(todo);
                }

                foreach (var eventModel in modelsEvents)
                {
                    stuffes.Add(eventModel);
                }

                return stuffes.OrderBy(s => s.Year).ThenBy(s => s.Mount).ThenBy(s => s.Day).ToList();
            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("byDate")]
        public ICollection<StuffListModel> ByDate(int day, int mounth, int year,
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
                DateTime searchDay = new DateTime(year, mounth, day);
                var todoes = this.Data.Todos.Where(t => t.OwnerId == user.Id && t.Date == searchDay).OrderBy(t => t.Priority);
                var events = this.Data.Events.Where(t => t.OwnerId == user.Id && t.StartDate == searchDay).OrderBy(t => t.Priority);

                var modelsTodo =
                    (from t in todoes
                     select new StuffListModel
                     {
                         Id = t.Id,
                         Title = t.Title,
                         Type = "todo"
                     });

                var modelsEvents =
                    (from e in events
                     select new StuffListModel
                     {
                         Id = e.Id,
                         Title = e.Title,
                         Type = "event"
                     });

                var stuffes = new List<StuffListModel>();

                foreach (var todo in modelsTodo)
                {
                    stuffes.Add(todo);
                }

                foreach (var eventModel in modelsEvents)
                {
                    stuffes.Add(eventModel);
                }

                return stuffes;
            });

            return responseMsg;
        }
    }
}
