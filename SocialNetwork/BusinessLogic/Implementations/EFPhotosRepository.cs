using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Interfaces;
using Domain;
using Domain.Entities;

namespace BusinessLogic.Implementations
{
    public class EFPhotosRepository : IPhotosRepository
    {
        private EFDbContext context;

        public EFPhotosRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Int32> GetPhotoIdsByUserId(Int32 userId)
        {
            return (from ph in context.Photos
                    where ph.UserId == userId
                    orderby ph.CreatedDate descending
                    select ph.Id).ToList();
        }

        public byte[] GetImageById(Int32 id)
        {
            return (from ph in context.Photos
                    where ph.Id == id
                    select ph.Image).FirstOrDefault();
        }

        public String GetCommentById(Int32 id)
        {
            return (from ph in context.Photos
                    where ph.Id == id
                    select ph.Comment).FirstOrDefault();
        }

        public DateTime GetCreatedDateById(Int32 id)
        {
            return (from ph in context.Photos
                    where ph.Id == id
                    select ph.CreatedDate).FirstOrDefault();
        }

        public void SavePhoto(Int32 userId, byte[] image, String comment, DateTime createdDate)
        {
            context.Photos.Add(new Photo
            {
                UserId = userId,
                Image = image,
                Comment = comment,
                CreatedDate = createdDate
            });
            context.SaveChanges();
        }

        public void DeletePhoto(Int32 id)
        {
            context.Photos.Remove((from ph in context.Photos
                                   where ph.Id == id
                                   select ph).FirstOrDefault());
            context.SaveChanges();
        }
    }
}