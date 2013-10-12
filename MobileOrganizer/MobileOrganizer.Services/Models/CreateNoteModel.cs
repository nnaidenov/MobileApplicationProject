using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileOrganizer.Services.Models
{
    public class CreateNoteModel
    {
        public string Text { get; set; }

        public string Title { get; set; }

        public ICollection<string> Images { get; set; }
    }
}