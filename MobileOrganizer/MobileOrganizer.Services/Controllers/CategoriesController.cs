using MobileOrganizer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MobileOrganizer.Models;

namespace MobileOrganizer.Services.Controllers
{
    public class CategoriesController : BaseApiController
    {
        public ICollection<string> GetAll()
        {
            return this.ExecuteOperationOrHandleExceptions(() =>
            {
                var context = new OrganizerDbContext();
                var categories = context.Categories
                                        .OrderBy(c => c.Name).Select(s => s.Name).ToList();
                                        

                return categories;
            });
        }
    }
}
