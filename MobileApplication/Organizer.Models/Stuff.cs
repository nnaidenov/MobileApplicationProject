using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Organizer.Models
{
    public class Stuff
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Todo> Todos { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

        public Stuff()
        {
            this.Events = new HashSet<Event>();
            this.Todos = new HashSet<Todo>();
            this.Notes = new HashSet<Note>();
        }
    }
}
