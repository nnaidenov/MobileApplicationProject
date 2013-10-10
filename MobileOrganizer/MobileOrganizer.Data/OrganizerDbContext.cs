using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileOrganizer.Models;

namespace MobileOrganizer.Data
{
    public class OrganizerDbContext : DbContext
    {
        public OrganizerDbContext() :
            base("OrganizerDb")
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Todo> Todos { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
