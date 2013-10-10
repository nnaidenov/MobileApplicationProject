using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Organizer.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public ICollection<string> ImagesUrls { get; set; }

        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int StuffId { get; set; }

        public virtual Stuff Stuff { get; set; }

        public Note()
        {
            this.ImagesUrls = new HashSet<string>();
        }
    }
}
