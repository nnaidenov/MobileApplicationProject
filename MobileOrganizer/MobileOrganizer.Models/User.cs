using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOrganizer.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string AuthCode { get; set; }

        public string SessionKey { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Todo> Todos { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

        public User()
        {
            this.Events = new HashSet<Event>();
            this.Todos = new HashSet<Todo>();
            this.Notes = new HashSet<Note>();
        }
    }
}
