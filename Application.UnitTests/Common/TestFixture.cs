using System;
using Persistence;
using Xunit;

namespace Application.UnitTests.Common
{
    public class TestFixture : IDisposable
    {
        public readonly ApplicationDbContext DbContext;
        
        public TestFixture()
        {
            DbContext = ApplicationDbContextFactory.Create();
        }
        
        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(DbContext);
        }
    }
    
    [CollectionDefinition("Application test collection")]
    public class ApplicationTestCollection : ICollectionFixture<TestFixture>{ }
}