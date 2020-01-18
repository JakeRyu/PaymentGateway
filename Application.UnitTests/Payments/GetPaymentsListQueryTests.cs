using Application.Common.Interfaces;
using Application.Features.Payments.Queries.GetPaymentsList;
using Application.UnitTests.Common;
using Xunit;

namespace Application.UnitTests.Payments
{
    [Collection("Application test collection")]
    public class GetPaymentsListQueryTests
    {
        private IApplicationDbContext Db { get; }

        public GetPaymentsListQueryTests(TestFixture testFixture)
        {
            Db = testFixture.Db;
        }

        [Fact]
        public void Handler_GivenValidRequest_ShouldReturnPaymentsList()
        {
            // Arrange
            var sut = new GetPaymentsListQuery.Handler(Db);

            // Act

            // Assert
        }
    }
}