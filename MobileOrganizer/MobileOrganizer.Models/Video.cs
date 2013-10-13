using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileOrganizer.Models
{
    public class Video
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public int NoteId { get; set; }

        public virtual Note Note { get; set; }
    }
}
