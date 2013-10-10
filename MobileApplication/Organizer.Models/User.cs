using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Authcode { get; set; }

        public string SessionKey { get; set; }

        public string Email { get; set; }

        public int StuffId { get; set; }

        public virtual Stuff Stuff { get; set; }

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
