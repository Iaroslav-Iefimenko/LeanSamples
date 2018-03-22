using System;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IFriendsRepository
    {
        IEnumerable<Int32> GetFriendIdsByUserId(Int32 userId); 
        //Проверяем, являются ли два пользователя друзьями
        Boolean UsersAreFriends(Int32 user1Id, Int32 user2Id);
        
        void AddFriend(Int32 userId, Int32 friendId);
        void DeleteFriend(Int32 userId, Int32 friendId);
    }
}
