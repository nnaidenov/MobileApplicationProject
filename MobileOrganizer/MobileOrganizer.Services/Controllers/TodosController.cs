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
    public class TodosController : BaseApiController
    {
        [HttpPost]
        [ActionName("create")]
        public HttpResponseMessage TodoCreate(CreateToDoModel model,
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
                    Title = model.Title,
                    IsDone = false
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

                var todoes = this.Data.Todos.Where(t => t.Date == date && t.OwnerId == user.Id);

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

        [HttpGet]
        [ActionName("all")]
        public ICollection<TodosListModel> All(
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

                var todos = this.Data.Todos.Where(t => t.OwnerId == user.Id && t.Date >= DateTime.Now);

                var modelsTodos =
                    (from t in todos
                     select new TodosListModel
                     {
                         Id = t.Id,
                         Title = t.Title,
                         Type = "todo"
                     });

                return modelsTodos.ToList();
            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("groupByDate")]
        public IQueryable GroupByDate(
             [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        {
            var responseMsg = ExecuteOperationOrHandleExceptions(
            () =>
            {
                var user = this.Data.Users.FirstOrDefault(u => u.SessionKey == sessionKey);
                if (user == null)
                {
                    throw new InvalidOperationException("Invalid email or password");
                }

                var todos = this.Data.Todos.Where(t => t.OwnerId == user.Id);
                
                var modelsTodos =
                    (from t in todos
                     select new TodosCalendarModel
                     {
                         Id = t.Id,
                         Date = t.Date
                     });

                return modelsTodos.GroupBy(g => g.Date);
            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("getById")]
        public ToDoModel GetById(int id,
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

                var todo = this.Data.Todos.Find(id);

                var model = new ToDoModel
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    Date = todo.Date,
                    IsDone = todo.IsDone
                };

                return model;
            });

            return responseMsg;
        }

        [HttpPost]
        [ActionName("update")]
        public bool UpdateById(int id,
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

                var todo = this.Data.Todos.Find(id);

                todo.IsDone = true;
                this.Data.SaveChanges();

                return true;
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

                var todo = this.Data.Todos.Find(id);
                this.Data.Todos.Remove(todo);
                this.Data.SaveChanges();

                return true;
            });

            return responseMsg;
        }
    }
}