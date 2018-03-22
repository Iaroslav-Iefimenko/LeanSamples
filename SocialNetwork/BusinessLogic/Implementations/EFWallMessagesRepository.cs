using BusinessLogic.Interfaces;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Implementations
{
    public class EFWallMessagesRepository : IWallMessagesRepository
    {
        private EFDbContext context;

        public EFWallMessagesRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Int32> GetWallMessagesIdsByUserId(int userId)
        {
            return (from wm in context.WallMessages
                where wm.WallOwnerId == userId
                orderby wm.CreatedDate descending 
                select wm.Id).ToList();
        }

        public Int32 GetWallMessageAuthorId(Int32 wmId)
        {
            return (from wm in context.WallMessages
                   where wm.Id == wmId
                   select wm.AuthorId).FirstOrDefault();
        }

        public DateTime GetWallMessageCreatedDate(Int32 wmId)
        {
            return (from wm in context.WallMessages
                    where wm.Id == wmId
                    select wm.CreatedDate).FirstOrDefault();
        }

        public String GetWallMessageText(Int32 wmId)
        {
            return (from wm in context.WallMessages
                    where wm.Id == wmId
                    select wm.Text).FirstOrDefault();
        }

        public void SaveWallMessage(Int32 authorId, Int32 wallOwnerId, String text, DateTime createdDate)
        {
            context.WallMessages.Add(new WallMessage
            {
                AuthorId = authorId,
                CreatedDate = createdDate,
                Text = text,
                WallOwnerId = wallOwnerId
            });
            context.SaveChanges();
        }

        public void DeleteWallMessage(Int32 wmId)
        {
            context.WallMessages.Remove((from wm in context.WallMessages
                                        where wm.Id == wmId
                                        select wm).FirstOrDefault());
            context.SaveChanges();
        }
    }
}
