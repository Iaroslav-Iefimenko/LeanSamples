using Domain.Entities;
using System;
using System.Data.Entity;

namespace Domain
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(String connectionString)
        {
            Database.Connection.ConnectionString = connectionString;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<WallMessage> WallMessages { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
