using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOrganizer.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Todo> Todos { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

        public Category()
        {
            this.Events = new HashSet<Event>();
            this.Todos = new HashSet<Todo>();
            this.Notes = new HashSet<Note>();
        }
    }
}
