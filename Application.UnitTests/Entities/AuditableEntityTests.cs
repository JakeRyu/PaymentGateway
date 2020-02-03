using System.Threading;
using Application.Common.Interfaces;
using Domain.Entities;
using Shouldly;
using TestCommon;
using Xunit;

namespace Application.UnitTests.Entities
{
    [Collection("Application test collection")]
    public class AuditableEntityTests
    {
        private IApplicationDbContext DbContext { get; }

        public AuditableEntityTests(TestFixture testFixture)
        {
            DbContext = testFixture.DbContext;
        }
        
        [Fact]
        public void CreatedOn_OnSaving_ShouldPopulate()
        {
            var payment = new Payment();
            
            DbContext.Payments.Add(payment);
            DbContext.SaveChangesAsync(CancellationToken.None);
            
            payment.CreatedOn.ShouldNotBeNull();
        }
    }
}