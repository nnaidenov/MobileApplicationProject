using Organizer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Organizer.API.Controllers
{
    public class BaseApiController : ApiController
    {
        protected IUnitOfWorkData Data;

        public BaseApiController(IUnitOfWorkData data)
        {
            this.Data = data;
        }

        public BaseApiController()
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