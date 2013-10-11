using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MobileOrganizer.Services.Models
{
    [DataContract]
    public class ToDoListModel
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }
    }
}