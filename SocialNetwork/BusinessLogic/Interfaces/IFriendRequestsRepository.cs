using System;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IFriendRequestsRepository
    {
        IEnumerable<Int32> GetIncomingRequestUserIdsByUserId(Int32 userId);
        IEnumerable<Int32> GetOutgoingRequestUserIdsByUserId(Int32 userId);
        //Отправил ли пользователь 1 пользователю 2 запрос на дружбу
        Boolean RequestIsSent(Int32 userFromId, Int32 userToId);
        void AddFriendRequest(Int32 userId, Int32 possibleFriendId);
        void DeleteFriendRequest(Int32 userId, Int32 possibleFriendId);
    }
}
