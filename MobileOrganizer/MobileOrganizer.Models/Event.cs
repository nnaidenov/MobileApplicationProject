using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOrganizer.Models
{
    public class Event
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsMuteMode { get; set; }

        public bool IsNotifyMode { get; set; }

        public Priority Priority { get; set; }

        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }
    }
}