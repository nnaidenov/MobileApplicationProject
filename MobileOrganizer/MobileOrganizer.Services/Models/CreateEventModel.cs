using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileOrganizer.Services.Models
{
    public class CreateEventModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public DateTime Reminder { get; set; }
    }
}