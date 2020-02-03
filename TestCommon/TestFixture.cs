using System;
using Application.Common.Mappings;
using AutoMapper;
using Persistence;
using Xunit;

namespace TestCommon
{
    public class TestFixture : IDisposable
    {
        public readonly ApplicationDbContext DbContext;
        public IMapper Mapper { get; private set; }

        public TestFixture()
        {
            DbContext = ApplicationDbContextFactory.Create();
            
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }
        
        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(DbContext);
        }
    }
    
    [CollectionDefinition("Application test collection")]
    public class ApplicationTestCollection : ICollectionFixture<TestFixture>{ }
}