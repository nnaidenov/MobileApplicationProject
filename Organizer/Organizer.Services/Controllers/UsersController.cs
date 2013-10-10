using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Organizer.Services.Controllers
{
    public class UsersController : ApiController
    {
        [HttpPost]
        [ActionName("register")]
        public HttpResponseMessage RegisterUser()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
