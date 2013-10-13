using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOrganizer.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public int NoteId { get; set; }

        public virtual Note Note { get; set; }
    }
}
