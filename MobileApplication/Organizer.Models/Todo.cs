using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Organizer.Models
{
    public class Todo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsNotifyMode { get; set; }

        public Priority Priority { get; set; }

        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int StuffId { get; set; }

        public virtual Stuff Stuff { get; set; }
    }
}
