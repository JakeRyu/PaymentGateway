using System;
using Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UnitTests.Common
{
    public static class ApplicationDbContextFactory
    {
        private static SqliteConnection _connection;

        public static ApplicationDbContext Create()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;
            
            var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureCreated();

            dbContext.Payments.AddRange(new[]
            {
                new Payment(1, "John Smith", "1111222233334444",
                    05, 2022, 298, 200, "GBP"), 
                new Payment(1, "Helen Smith", "1111222233334444",
                    09, 2022, 312, 100, "GBP") 
            });

            dbContext.SaveChanges();
            
            return dbContext;
        }

        public static void Destroy(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}