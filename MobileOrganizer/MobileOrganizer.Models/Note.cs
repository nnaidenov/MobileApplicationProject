﻿using System;
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

        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<Image> ImagesUrls { get; set; }

        public virtual ICollection<Video> VideosUrls { get; set; }

        public Note()
        {
            this.ImagesUrls = new HashSet<Image>();
            this.VideosUrls = new HashSet<Video>();
        }
    }
}
