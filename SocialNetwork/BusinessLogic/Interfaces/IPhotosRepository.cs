using System;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IPhotosRepository
    {
        IEnumerable<Int32> GetPhotoIdsByUserId(Int32 userId);
        byte[] GetImageById(Int32 id);
        String GetCommentById(Int32 id);
        DateTime GetCreatedDateById(Int32 id);

        void SavePhoto(Int32 userId, byte[] image, String comment, DateTime createdDate);
        void DeletePhoto(Int32 id);
    }
}