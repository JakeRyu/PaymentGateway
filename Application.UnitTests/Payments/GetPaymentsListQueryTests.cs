using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Features.Payments.Queries.GetPaymentsList;
using Application.UnitTests.Common;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Payments
{
    [Collection("Application test collection")]
    public class GetPaymentsListQueryTests
    {
        private IApplicationDbContext DbContext { get; }
        private static Guid MerchantId => Guid.NewGuid();

        public GetPaymentsListQueryTests(TestFixture testFixture)
        {
            DbContext = testFixture.DbContext;
        }

        [Fact]
        public async Task Handler_GivenValidRequest_ShouldReturnPaymentsList()
        {
            // Arrange
            var query = new GetPaymentsListQuery
            {
                MerchantId = MerchantId
            };
            var sut = new GetPaymentsListQuery.Handler(DbContext);

            // Act
            var result = await sut.Handle(query, CancellationToken.None);
            
            // Assert
            result.ShouldBeOfType<PaymentsListVm>();
            result.Payments.Count.ShouldBe(2);
        }

    }
}