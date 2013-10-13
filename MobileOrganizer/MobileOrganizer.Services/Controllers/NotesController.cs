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

                if (model.Videos != null)
                {
                    var videos = new List<Video>();
                    foreach (var video in model.Videos)
                    {
                        var newVideo = new Video { Path = video, NoteId = newNote.Id, Note = newNote };
                        videos.Add(newVideo);
                    }

                    newNote.VideosUrls = videos;
                }

                this.Data.Notes.Add(newNote);
                this.Data.SaveChanges();

                var response = this.Request.CreateResponse(HttpStatusCode.Created);

                return response;
            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("all")]
        public ICollection<NotesListModel> All(
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

                var notes = this.Data.Notes.Where(t => t.OwnerId == user.Id);

                var modelsTodos =
                    (from n in notes
                     select new NotesListModel
                     {
                         Id = n.Id,
                         Title = n.Title,
                         Type = "note"
                     });

                return modelsTodos.ToList();
            });

            return responseMsg;
        }

        [HttpGet]
        [ActionName("getById")]
        public NoteModel GetById(int id,
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

                var note = this.Data.Notes.Find(id);

                var model = new NoteModel
                {
                    Title = note.Title,
                    Description = note.Text,
                    ImagesUrls = (from i in note.ImagesUrls
                                  select new ImageModel
                                  {
                                      Path = i.Path
                                  }),
                    VideosUrls = (from v in note.VideosUrls
                                  select new VideoModel
                                  {
                                      Path = v.Path
                                  })
                };

                return model;
            });

            return responseMsg;
        }
    }
}
