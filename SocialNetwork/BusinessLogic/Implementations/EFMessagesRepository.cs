using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain;
using BusinessLogic.Interfaces;
using System.Data;

namespace BusinessLogic.Implementations
{
    public class EFMessagesRepository : IMessagesRepository
    {
        private EFDbContext context;

        public EFMessagesRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Int32> GetIncomingMessageIdsByUserId(Int32 userId)
        {
            return (from im in context.Messages
                    where im.UserToId == userId
                    orderby im.CreatedDate descending
                    select im.Id).ToList();
        }

        public IEnumerable<Int32> GetOutgoingMessageIdsByUserId(Int32 userId)
        {
            return (from om in context.Messages
                    where om.UserFromId == userId
                    orderby om.CreatedDate descending
                    select om.Id).ToList();
        }

        public String GetMessageTextById(Int32 id)
        {
            return (from im in context.Messages
                    where im.Id == id
                    select im.Text).FirstOrDefault();
        }

        public DateTime GetMessageCreatedDateById(Int32 id)
        {
            return (from im in context.Messages
                    where im.Id == id
                    select im.CreatedDate).FirstOrDefault();
        }

        public Int32 GetMessageUserFromIdById(Int32 id)
        {
            return (from im in context.Messages
                    where im.Id == id
                    select im.UserFromId).FirstOrDefault();
        }

        public Int32 GetMessageUserToIdById(Int32 id)
        {
            return (from im in context.Messages
                    where im.Id == id
                    select im.UserToId).FirstOrDefault();
        }

        public void DeleteMessage(Int32 messId)
        {
            context.Messages.Remove(
                (from im in context.Messages where im.Id == messId select im).FirstOrDefault());
            context.SaveChanges();
        }
        
        public void SaveOutgoingMessage(Int32 messId, Int32 userId, Int32 userToId, String text, DateTime createdDate)
        {
            if (messId == 0)
                context.Messages.Add(new Message
                {
                    Id = messId,
                    UserFromId = userId,
                    UserToId = userToId,
                    Text = text,
                    CreatedDate = createdDate
                });
            else
            {
                Message mess =
                    (from om in context.Messages where om.Id == messId select om).FirstOrDefault();
                if (mess != null)
                {
                    mess.Text = text;
                    context.Entry(mess).State = EntityState.Modified;
                }
            }
            context.SaveChanges();
        }
    }
}
