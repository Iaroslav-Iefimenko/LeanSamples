using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private DataManager dataManager;

        public MessagesController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public ActionResult Index()
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return RedirectToAction("Index", "Account");
            int currentId = (int?)user.ProviderUserKey ?? 0;
            //готовим все входящие сообщения и их авторов
            List<IncomingMessageViewModel> model = new List<IncomingMessageViewModel>();
            foreach(Int32 id in dataManager.Messages.GetIncomingMessageIdsByUserId(currentId) )
            {
                Int32 userFromId = dataManager.Messages.GetMessageUserFromIdById(id);
                model.Add(new IncomingMessageViewModel 
                { 
                    Id = id,
                    UserId = currentId,
                    Text = dataManager.Messages.GetMessageTextById(id),
                    CreatedDate = dataManager.Messages.GetMessageCreatedDateById(id),
                    UserFromId = userFromId,
                    UserFromFirstName = dataManager.Users.GetFirstNameById(userFromId),
                    UserFromLastName = dataManager.Users.GetLastNameById(userFromId)
                });
            }
            
            return View(model);
        }

        public ActionResult Outgoing()
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return RedirectToAction("Index", "Account");
            int currentId = (int?)user.ProviderUserKey ?? 0;
            
            //готовим все исходящие сообщения и их авторов
            List<OutgoingMessageViewModel> model = new List<OutgoingMessageViewModel>();
            foreach (Int32 id in dataManager.Messages.GetOutgoingMessageIdsByUserId(currentId))
            {
                Int32 userToId = dataManager.Messages.GetMessageUserToIdById(id);
                model.Add(new OutgoingMessageViewModel
                {
                    Id = id,
                    UserId = currentId,
                    Text = dataManager.Messages.GetMessageTextById(id),
                    CreatedDate = dataManager.Messages.GetMessageCreatedDateById(id),
                    UserToId = userToId,
                    UserToFirstName = dataManager.Users.GetFirstNameById(userToId),
                    UserToLastName = dataManager.Users.GetLastNameById(userToId)
                });
            }

            return View(model); 
        }

        public ActionResult NewMessage(Int32 userToId)
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return RedirectToAction("Index", "Account");
            //Перед отправкой определить автора, адресата и дату создания сообщения
            return View(new OutgoingMessageViewModel 
            {
                Id = 0,
                UserId = (int?)user.ProviderUserKey ?? 0,
                UserToId = userToId,
                CreatedDate = DateTime.Now,
                UserToFirstName = dataManager.Users.GetFirstNameById(userToId),
                UserToLastName = dataManager.Users.GetLastNameById(userToId)
            });
        }

        [HttpPost]
        public ActionResult NewMessage(OutgoingMessageViewModel message)
        {
            if (ModelState.IsValid)
            { 
                dataManager.Messages.SaveOutgoingMessage(message.Id, 
                    message.UserId, message.UserToId, message.Text, message.CreatedDate);

                return RedirectToAction("Index", "Home");
            }
            return View(message);
        }
    }
}
