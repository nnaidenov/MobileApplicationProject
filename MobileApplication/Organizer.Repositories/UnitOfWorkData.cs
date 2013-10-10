using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organazer.Data;
using Organizer.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Organizer.Repositories
{
    public class UnitOfWorkData : IUnitOfWorkData
    {
        private readonly OrganizerDbContex dbContext;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UnitOfWorkData()
            : this(new OrganizerDbContex())
        {
        }

        public UnitOfWorkData(OrganizerDbContex context)
        {
            this.dbContext = context;
        }

        public IRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public IRepository<Event> Events
        {
            get
            {
                return this.GetRepository<Event>();
            }
        }

        public IRepository<Stuff> Stuffes
        {
            get
            {
                return this.GetRepository<Stuff>();
            }
        }

        public IRepository<Todo> Todos
        {
            get
            {
                return this.GetRepository<Todo>();
            }
        }

        public IRepository<Note> Notes
        {
            get
            {
                return this.GetRepository<Note>();
            }
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public int SaveChanges()
        {
            try
            {
                return this.dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                return this.dbContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EfRepository<T>);

                this.repositories.Add(typeof(T),
                    Activator.CreateInstance(type, this.dbContext));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
