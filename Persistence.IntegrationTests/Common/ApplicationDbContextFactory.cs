using System;
using Common.DateService;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Persistence.IntegrationTests.Commont
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

            return dbContext;
        }

        public static void Destroy(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}