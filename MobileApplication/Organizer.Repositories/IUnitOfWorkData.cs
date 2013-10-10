using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizer.Models;

namespace Organizer.Repositories
{
    public interface IUnitOfWorkData
    {
        IRepository<Category> Categories { get; }

        IRepository<Event> Events { get; }

        IRepository<Note> Notes { get; }

        IRepository<Stuff> Stuffes { get; }

        IRepository<Todo> Todos { get; }

        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
