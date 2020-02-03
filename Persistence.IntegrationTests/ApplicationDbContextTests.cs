using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Persistence.IntegrationTests
{
    [Collection("Application test collection")]
    public class ApplicationDbContextTests
    {
        private ApplicationDbContext _sut;

        public ApplicationDbContextTests(TestFixture testFixture)
        {
            _sut = testFixture.DbContext;

            _sut.Payments.Add(new Payment(Guid.NewGuid(), 1, "John Smith",
                "1111222233334444", "05/20", "298", 200, "GBP"));

            _sut.SaveChanges();
        }

        [Fact]
        public async Task SaveChangesAsync_GivenNewPayment_ShouldSetCreatedOnProperty()
        {
            var payment = await _sut.Payments.FirstAsync(CancellationToken.None);

            payment.CreatedOn.ShouldNotBeNull();
        }

        [Fact]
        public async Task SaveChangesAsync_WhenUpdatingExistingPayment_ShouldSetLastModifiedOnProperty()
        {
            var payment = await _sut.Payments.FirstAsync(CancellationToken.None);
            payment.UpdateCardHolderName("Jane Smith");

            await _sut.SaveChangesAsync(CancellationToken.None);

            payment.LastModifiedOn.ShouldNotBeNull();
        }
    }
}