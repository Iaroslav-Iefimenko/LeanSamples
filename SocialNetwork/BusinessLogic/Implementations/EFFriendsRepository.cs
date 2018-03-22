using BusinessLogic.Interfaces;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Implementations
{
    public class EFFriendsRepository : IFriendsRepository
    {
        private EFDbContext context;

        public EFFriendsRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Int32> GetFriendIdsByUserId(Int32 userId)
        {
            return (from f in context.Friends 
                    where f.UserId == userId
                    select f.FriendId).ToList();
        }

        public bool UsersAreFriends(int user1Id, int user2Id)
        {
            return (from f in context.Friends
                where f.UserId == user1Id && f.FriendId == user2Id
                select f.Id).Count() != 0;
        }

        public void AddFriend(Int32 userId, Int32 friendId)
        {
            context.Friends.Add(new Friend
            {
                UserId = userId,
                FriendId = friendId
            });
            context.SaveChanges();
        }

        public void DeleteFriend(Int32 userId, Int32 friendId)
        {
            Friend friend = (from fr in context.Friends
                where fr.UserId == userId && fr.FriendId == friendId
                select fr).FirstOrDefault();
            if (friend != null)
                context.Friends.Remove(friend);
            context.SaveChanges();
        }
    }
}
