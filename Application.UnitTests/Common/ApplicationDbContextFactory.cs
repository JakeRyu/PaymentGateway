using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UnitTests.Common
{
    public class ApplicationDbContextFactory
    {
        private static SqliteConnection _connection;

        private ApplicationDbContextFactory()
        {
        }
        
        public static ApplicationDbContext Create()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;
            
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}