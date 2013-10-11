﻿using MobileOrganizer.Models;
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
    public class TodosController : BaseApiController
    {
        [HttpPost]
        [ActionName("create")]
        public HttpResponseMessage PostPost(CreateToDoModel model,
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

                var newToDo = new Todo
                {
                    Date = model.Date,
                    Description = model.Description,
                    Owner = user,
                    OwnerId = user.Id,
                    Title = model.Title
                };

                this.Data.Todos.Add(newToDo);
                this.Data.SaveChanges();

                var response = this.Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = newToDo.Id }));

                return response;
            });

            return responseMsg;
        }

        [HttpGet]
        public IQueryable<ToDoListModel> GetByDate(DateTime date,
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

                    var todoes = this.Data.Todos.Where(t => t.Date == date && t.OwnerId == user.Id).OrderByDescending(t => t.Priority);

                var models =
                    (from t in todoes
                     select new ToDoListModel
                     {
                         Title = t.Title
                     });

                return models;
            });

            return responseMsg;
        }
    }
}
