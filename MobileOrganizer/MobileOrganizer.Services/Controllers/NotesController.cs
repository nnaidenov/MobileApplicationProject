using MobileOrganizer.Models;
using MobileOrganizer.Services.Attributes;
using MobileOrganizer.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ValueProviders;

namespace MobileOrganizer.Services.Controllers
{
    public class NotesController : BaseApiController
    {
        [HttpPost]
        [ActionName("create")]
        public HttpResponseMessage PostPost(CreateNoteModel model,
             [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        {
            var responseMsg = ExecuteOperationOrHandleExceptions(
            () =>
            {
                var user = this.Data.Users.FirstOrDefault(u => u.SessionKey == sessionKey);
                if (user == null)
                {
                    throw new InvalidOperationException("Invalid username or password");
                }
               

                var newNote = new Note
                {
                    Owner = user,
                    OwnerId = user.Id,
                    Text = model.Text,
                    Title = model.Title
                };

                if (model.Images != null)
                {
                    var images = new List<Image>();
                    foreach (var image in model.Images)
                    {
                        var newImage = new Image { Path = image, NoteId = newNote.Id, Note = newNote };
                        images.Add(newImage);
                    }

                    newNote.ImagesUrls = images;
                }

                this.Data.Notes.Add(newNote);
                this.Data.SaveChanges();

                var response = this.Request.CreateResponse(HttpStatusCode.Created);
               
                return response;
            });

            return responseMsg;
        }
    }
}
