using System;
using System.Linq;
using Application.Common.Interfaces;
using Common.DateService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Persistence;
using TestCommon;

namespace Api.IntegrationTests.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices(services =>
                {
                    // Remove the Api's ApplicationDbContext registration.
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                             typeof(DbContextOptions<ApplicationDbContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }
                    
                    // Create a new service provider.
                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();
                    
                    var dateTimeMock = new Mock<IDateTime>();
                    dateTimeMock.Setup(x => x.Now).Returns(DateTime.Now);
                    services.AddScoped<IDateTime>(provider => dateTimeMock.Object);

                    // Add a database context using an in-memory 
                    // database for testing.
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    services.AddScoped<IApplicationDbContext>(provider => ApplicationDbContextFactory.Create());

                    var sp = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();

                    // Ensure the database is created.
                    dbContext.Database.EnsureCreated();
                })
                .UseEnvironment("Test");
        }
    }
}