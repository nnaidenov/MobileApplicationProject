using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Organizer.API.Models
{
    public class UserLoggedModel
    {
        public string Username { get; set; }

        public string SessionKey { get; set; }
    }
}