using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class UserViewModel
    {
        //Пользователь
        public Int32 Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String MiddleName { get; set; }

        //Вызываемый пользователь - это я сам
        public Boolean UserIsMe { get; set; }

        //Вызываемый пользователь - это мой друг
        public Boolean UserIsMyFriend { get; set; }

        //Заявка в друзья уже отправлена
        public Boolean FriendRequestIsSent { get; set; }

        //Стена
        public IEnumerable<WallMessageViewModel> WallMessages { get; set; }
    }
}