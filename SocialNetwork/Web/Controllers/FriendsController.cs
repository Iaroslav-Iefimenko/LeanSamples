using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        private DataManager dataManager;

        public FriendsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        private void FillUsersList(IEnumerable<Int32> userIds, ref IEnumerable<UserViewModel> users)
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return;

            //выбираем друзей
            foreach (Int32 id in userIds)
            {
                ((List<UserViewModel>)users).Add(new UserViewModel
                {
                    Id = id,
                    FirstName = dataManager.Users.GetFirstNameById(id),
                    LastName = dataManager.Users.GetLastNameById(id),
                    MiddleName = dataManager.Users.GetMiddleNameById(id),
                    UserIsMe = id == ((int?)user.ProviderUserKey ?? 0),
                    UserIsMyFriend = dataManager.Friends.UsersAreFriends(((int?)user.ProviderUserKey ?? 0), id),
                    FriendRequestIsSent = dataManager.FriendRequests.RequestIsSent(id, ((int?)user.ProviderUserKey ?? 0))
                });
            }
        }

        public ActionResult Index()
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return RedirectToAction("Index", "Account");
            
            Int32 currentUserId = (int?)user.ProviderUserKey ?? 0;
            FriendsViewModel model = new FriendsViewModel();

            //выбираем друзей
            IEnumerable<UserViewModel> friends = new List<UserViewModel>();
            FillUsersList(dataManager.Friends.GetFriendIdsByUserId(currentUserId),
                ref friends);
            model.Friends = friends;
            
            //выбираем входящие заявки
            IEnumerable<UserViewModel> incomingRequests = new List<UserViewModel>(); 
            FillUsersList(dataManager.FriendRequests.GetIncomingRequestUserIdsByUserId(currentUserId),
                ref incomingRequests);
            model.IncomingRequests = incomingRequests;

            //выбираем исходящие заявки
            IEnumerable<UserViewModel> outgoingRequests = new List<UserViewModel>();
            FillUsersList(dataManager.FriendRequests.GetOutgoingRequestUserIdsByUserId(currentUserId),
                ref outgoingRequests);
            model.OutgoingRequests = outgoingRequests;

            return View(model);
        }

        //пользователь отправляет свою заявку в друзья
        public ActionResult SendFriendRequest(int id)
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return RedirectToAction("Index", "Account");

            dataManager.FriendRequests.AddFriendRequest((int?)user.ProviderUserKey ?? 0, id);

            return RedirectToAction("Index", "Home", new { id });
        }

        //пользователь удаляет свою заявку в друзья
        public ActionResult CancelFriendRequest(int id)
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return RedirectToAction("Index", "Account");
            
            dataManager.FriendRequests.DeleteFriendRequest( (int?)user.ProviderUserKey ?? 0, id) ;

            return RedirectToAction("Index", "Home", new { id });
        }

        //пользователь отклоняет заявку в друзья от другого пользователя
        public ActionResult RejectFriendRequest(int id)
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return RedirectToAction("Index", "Account");

            dataManager.FriendRequests.DeleteFriendRequest(id, (int?)user.ProviderUserKey ?? 0);

            return RedirectToAction("Index", "Home", new { id });
        }

        //пользователь подтверждает заявку в друзья от другого пользователя
        public ActionResult ConfirmFriendRequest(int id)
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return RedirectToAction("Index", "Account");

            Int32 currentUserId = (int?)user.ProviderUserKey ?? 0;
            //специально создаем дублирующую запись в отношениях "Друзья" для обоих пользователей
            dataManager.Friends.AddFriend(id, currentUserId);
            dataManager.Friends.AddFriend(currentUserId, id);
            //удаляем ненужную заявку в друзья
            dataManager.FriendRequests.DeleteFriendRequest(id, currentUserId);
            dataManager.FriendRequests.DeleteFriendRequest(currentUserId, id);

            return RedirectToAction("Index", "Home", new { id });
        }

        //пользователь удаляет другого пользователя из друзей
        public ActionResult DeleteFriend(int id)
        {
            MembershipUser user = Membership.GetUser();
            if (user == null)
                return RedirectToAction("Index", "Account");

            Int32 currentUserId = (int?)user.ProviderUserKey ?? 0;
            //удаляем две дублирующие записи про друзей, необходимо удалить обе
            dataManager.Friends.DeleteFriend(id, currentUserId);
            dataManager.Friends.DeleteFriend(currentUserId, id);

            return RedirectToAction("Index", "Home", new { id });
        }
    }
}
