using System;
using Common.DateService;
using Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
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

            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(x => x.Now).Returns(DateTime.Now);
            
            var dbContext = new ApplicationDbContext(options, dateTimeMock.Object);
            dbContext.Database.EnsureCreated();

            dbContext.Payments.AddRange(new[]
            {
                new Payment(Guid.NewGuid(), 1, "John Smith", "1111222233334444",
                    "05/20", "298", 200, "GBP"), 
                new Payment(Guid.NewGuid(),1, "Helen Smith", "1111222233334444",
                    "09/20", "312", 100, "GBP") 
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