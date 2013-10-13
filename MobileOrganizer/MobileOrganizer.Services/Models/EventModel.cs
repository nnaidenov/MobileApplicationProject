using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileOrganizer.Services.Models
{
    public class EventModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EnddDate { get; set; }

        public int Id { get; set; }
    }
}