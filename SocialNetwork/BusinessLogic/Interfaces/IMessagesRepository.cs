using System;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IMessagesRepository
    {
        IEnumerable<Int32> GetIncomingMessageIdsByUserId(Int32 userId);
        IEnumerable<Int32> GetOutgoingMessageIdsByUserId(Int32 userId);

        void SaveOutgoingMessage(Int32 messId, Int32 userId, Int32 userToId, String text, DateTime createdDate);
        void DeleteMessage(Int32 messId);
        
        Int32 GetMessageUserFromIdById(Int32 id);
        Int32 GetMessageUserToIdById(Int32 id);
        String GetMessageTextById(Int32 id);
        DateTime GetMessageCreatedDateById(Int32 id);
    }
}
