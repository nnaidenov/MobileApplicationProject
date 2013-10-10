using System.Collections.Generic;
namespace Organizer.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentCategoryId { get; set; }

        public virtual Category ParentCategory { get; set; }

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