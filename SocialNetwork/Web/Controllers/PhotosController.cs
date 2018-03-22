using BusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class PhotosController : Controller
    {
        private DataManager dataManager;

        public PhotosController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public ActionResult Index(Int32 id = 0)
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return RedirectToAction("Index", "Account");
            int currentId = (int?)user.ProviderUserKey ?? 0;
           
            if (id == 0)
            {
                return RedirectToAction("Index", new { id = currentId });
            }

            PhotosViewModel model = new PhotosViewModel
            {
                UserId = id,
                UserName = dataManager.Users.GetFirstNameById(id) + " " +
                           dataManager.Users.GetLastNameById(id),
                Photos = new List<PhotoViewModel>(),
                UserIsMe = id == currentId
            };
            foreach (Int32 photoId in dataManager.Photos.GetPhotoIdsByUserId(id))
            {
                model.Photos.Add(new PhotoViewModel
                {
                    Id = photoId,
                    UserId = id,
                    Image = dataManager.Photos.GetImageById(photoId),
                    Comment = dataManager.Photos.GetCommentById(photoId),
                    CreatedDate = dataManager.Photos.GetCreatedDateById(photoId)
                });
            }

            return View(model);
        }

        public ActionResult NewPhoto()
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return RedirectToAction("Index", "Account");
            
            //Перед отправкой определить автора и дату создания сообщения
            return View(new PhotoViewModel
            {
                UserId = (int?)user.ProviderUserKey ?? 0,
                CreatedDate = DateTime.Now
            });
        }

        [HttpPost]
        public ActionResult NewPhoto([ImageBind("Image")]PhotoViewModel photoModel)
        {
            if (ModelState.IsValid)
            {
                dataManager.Photos.SavePhoto(photoModel.UserId, photoModel.Image,
                        photoModel.Comment, photoModel.CreatedDate);

                return RedirectToAction("Index", "Photos",new {id = photoModel.UserId});
            }
            return View(photoModel);
        }

        //пользователь удаляет свою фотографию
        public ActionResult DeletePhoto(Int32 id, Int32 photoId)
        {
            dataManager.Photos.DeletePhoto(photoId);
            return RedirectToAction("Index", "Photos", new { id });
        }

        [OutputCache(Duration = 0)]
        public ActionResult Image(int id)
        {
            return new FileStreamResult(new MemoryStream(dataManager.Photos.GetImageById(id)), "image/png");
        }
    }
}
