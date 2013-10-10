using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organazer.Data
{
    public class OrganizerDbContex : DbContext 
    {
        public OrganizerDbContex() :
            base("OrganizerDbb")
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Stuff> Stuffs { get; set; }

        public DbSet<Todo> Todos { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
