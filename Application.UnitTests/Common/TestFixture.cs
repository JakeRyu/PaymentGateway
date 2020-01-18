using System;
using Persistence;
using Xunit;

namespace Application.UnitTests.Common
{
    public class TestFixture : IDisposable
    {
        public readonly ApplicationDbContext Db;
        
        public TestFixture()
        {
            Db = ApplicationDbContextFactory.Create();
        }
        
        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(Db);
        }
    }
    
    [CollectionDefinition("Application test collection")]
    public class ApplicationTestCollection : ICollectionFixture<TestFixture>{ }
}