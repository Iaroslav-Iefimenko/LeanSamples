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
        public DbSet<GameResult> GameResults { get; set; }
        public DbSet<GameSetting> GameSettings { get; set; }
    }
}
