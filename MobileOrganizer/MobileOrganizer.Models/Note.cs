using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOrganizer.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public ICollection<string> ImagesUrls { get; set; }

        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public Note()
        {
            this.ImagesUrls = new HashSet<string>();
        }
    }
}
