using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileOrganizer.Services.Models
{
    public class NoteModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IEnumerable<ImageModel> ImagesUrls { get; set; }

        public IEnumerable<VideoModel> VideosUrls { get; set; }

        public int Id { get; set; }
    }
}