using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private DataManager dataManager;

        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public ActionResult Index(Int32 id = 0)
        {
            //если в адресе не было ID пользователя, 
            //то явным образом вычисляем Id и перенаправляем пользователя на то же действие с вычисленным ID
            MembershipUser msUser = Membership.GetUser();
            if (msUser == null)
                return RedirectToAction("Index", "Account");
            
            if (id == 0)
                return RedirectToAction("Index", new { id = msUser.ProviderUserKey });
            
            //заполняем модель для пользователя
            UserViewModel model = new UserViewModel
            {
                Id = id,
                FirstName = dataManager.Users.GetFirstNameById(id),
                LastName = dataManager.Users.GetLastNameById(id),
                MiddleName = dataManager.Users.GetMiddleNameById(id),
                UserIsMe = id == ((int?)msUser.ProviderUserKey ?? 0),
                UserIsMyFriend = dataManager.Friends.UsersAreFriends(((int?)msUser.ProviderUserKey ?? 0), id),
                FriendRequestIsSent = dataManager.FriendRequests.RequestIsSent(((int?)msUser.ProviderUserKey ?? 0), id),
                WallMessages = new List<WallMessageViewModel>()
            };

            foreach (Int32 wmId in dataManager.WallMessages.GetWallMessagesIdsByUserId(id))
            {
                Int32 authorId = dataManager.WallMessages.GetWallMessageAuthorId(wmId);
                WallMessageViewModel message = new WallMessageViewModel
                {
                    Id = wmId,
                    WallOwnerId = id,
                    AuthorId = authorId,
                    AuthorName = dataManager.Users.GetFirstNameById(authorId) + " " + dataManager.Users.GetLastNameById(authorId),
                    CreatedDate = dataManager.WallMessages.GetWallMessageCreatedDate(wmId),
                    Text = dataManager.WallMessages.GetWallMessageText(wmId)
                };
                ((List<WallMessageViewModel>)model.WallMessages).Add(message);
            }

            return View(model);
        }

        public ActionResult NewWallMessage(Int32 wallOwnerId)
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return RedirectToAction("Index", "Account");
            
            int currentId = (int?)user.ProviderUserKey ?? 0;
            //Перед отправкой определить автора, адресата и дату создания сообщения
            return View(new WallMessageViewModel
            {
                AuthorId = currentId,
                AuthorName = dataManager.Users.GetFirstNameById(currentId) + " " + dataManager.Users.GetLastNameById(currentId),
                WallOwnerId = wallOwnerId,
                CreatedDate = DateTime.Now
            });
        }

        [HttpPost]
        public ActionResult NewWallMessage(WallMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                dataManager.WallMessages.SaveWallMessage(model.AuthorId,model.WallOwnerId, model.Text, model.CreatedDate);
                return RedirectToAction("Index", "Home", new { id = model.WallOwnerId });
            }
            return View(model);
        }
        
        //пользователь удаляет свое сообщение на стене
        public ActionResult DeleteWallMessage(Int32 id, Int32 messageId)
        {
            dataManager.WallMessages.DeleteWallMessage(messageId);
           return RedirectToAction("Index", "Home", new { id });
        }

    }
}
