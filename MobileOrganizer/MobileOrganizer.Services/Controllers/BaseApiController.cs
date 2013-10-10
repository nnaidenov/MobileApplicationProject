using MobileOrganizer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobileOrganizer.Services.Controllers
{
    public class BaseApiController : ApiController
    {
        public OrganizerDbContext Data = new OrganizerDbContext();

        protected T ExecuteOperationOrHandleExceptions<T>(Func<T> operation)
        {
            try
            {
                return operation();
            }
            catch (Exception ex)
            {
                var errResponse = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                throw new HttpResponseException(errResponse);
            }
        }
    }
}
