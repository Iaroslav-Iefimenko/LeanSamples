using BusinessLogic.Interfaces;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Implementations
{
    public class EFFriendRequestsRepository : IFriendRequestsRepository
    {
        private EFDbContext context;

        public EFFriendRequestsRepository(EFDbContext context)
        {
            this.context = context;
        }
        
        public IEnumerable<Int32> GetIncomingRequestUserIdsByUserId(Int32 userId)
        {
            return (from fr in context.FriendRequests
                        where fr.PossibleFriendId == userId
                        select fr.UserId).ToList();
        }

        public IEnumerable<Int32> GetOutgoingRequestUserIdsByUserId(Int32 userId)
        {
            return (from fr in context.FriendRequests
                    where fr.UserId == userId
                    select fr.PossibleFriendId).ToList();
        }

        public bool RequestIsSent(int userFromId, int userToId)
        {
            return (from fr in context.FriendRequests
                    where fr.UserId == userFromId && fr.PossibleFriendId == userToId
                    select fr.Id).Count() != 0;
        }

        public void AddFriendRequest(Int32 userId, Int32 possibleFriendId)
        {
            context.FriendRequests.Add(new FriendRequest
            {
                UserId = userId,
                PossibleFriendId = possibleFriendId
            });
            context.SaveChanges();
        }

        public void DeleteFriendRequest(Int32 userId, Int32 possibleFriendId)
        {
            FriendRequest req = (from fr in context.FriendRequests
                where fr.UserId == userId && fr.PossibleFriendId == possibleFriendId
                select fr).FirstOrDefault();
            if (req != null)
                context.FriendRequests.Remove(req);
            context.SaveChanges();
        }
    }
}
