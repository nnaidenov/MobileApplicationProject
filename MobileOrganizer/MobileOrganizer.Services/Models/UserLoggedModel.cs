using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MobileOrganizer.Services.Models
{
    [DataContract]
    public class UserLoggedModel
    {
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "sessionKey")]
        public string SessionKey { get; set; }
    }
}