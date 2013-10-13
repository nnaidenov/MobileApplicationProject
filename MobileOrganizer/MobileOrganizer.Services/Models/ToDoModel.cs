using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileOrganizer.Services.Models
{
    public class ToDoModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsDone { get; set; }
    }
}