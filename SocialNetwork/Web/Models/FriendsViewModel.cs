using System.Collections.Generic;

namespace Web.Models
{
    public class FriendsViewModel
    {
        //Друзья
        public IEnumerable<UserViewModel> Friends { get; set; }

        //Входящие заявки в друзья
        public IEnumerable<UserViewModel> IncomingRequests { get; set; }

        //Исходящие заявки в друзья
        public IEnumerable<UserViewModel> OutgoingRequests { get; set; }
    }
}