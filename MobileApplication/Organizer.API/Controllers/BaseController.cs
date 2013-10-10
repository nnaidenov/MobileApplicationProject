using Organizer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Organizer.API.Controllers
{
    public class BaseController : ApiController
    {
            protected IUnitOfWorkData Data;

        public BaseController(IUnitOfWorkData data)
        {
            this.Data = data;
        }

        public BaseController()
            : this(new UnitOfWorkData())
        {
        }

        protected T ExceptionHandler<T>(Func<T> operation)
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
