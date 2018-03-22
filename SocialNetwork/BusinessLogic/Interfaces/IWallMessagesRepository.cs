using System;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IWallMessagesRepository
    {
        IEnumerable<Int32> GetWallMessagesIdsByUserId(Int32 userId);
        Int32 GetWallMessageAuthorId(Int32 wmId);
        DateTime GetWallMessageCreatedDate(Int32 wmId);
        String GetWallMessageText(Int32 wmId);

        void SaveWallMessage(Int32 authorId, Int32 wallOwnerId, String text, DateTime createdDate);
        void DeleteWallMessage(Int32 wmId);
    }
}
