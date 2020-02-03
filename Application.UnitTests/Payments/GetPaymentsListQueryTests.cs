using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Features.Payments.Queries.GetPaymentsList;
using AutoMapper;
using Shouldly;
using TestCommon;
using Xunit;

namespace Application.UnitTests.Payments
{
    [Collection("Application test collection")]
    public class GetPaymentsListQueryTests
    {
        private IApplicationDbContext DbContext { get; }
        private IMapper Mapper { get; }

        public GetPaymentsListQueryTests(TestFixture testFixture)
        {
            DbContext = testFixture.DbContext;
            Mapper = testFixture.Mapper;
        }

        [Fact]
        public async Task Handler_GivenValidRequest_ShouldReturnPaymentsList()
        {
            // Arrange
            var query = new GetPaymentsListQuery
            {
                MerchantId = 1
            };
            var sut = new GetPaymentsListQuery.Handler(DbContext, Mapper);

            // Act
            var result = await sut.Handle(query, CancellationToken.None);
            
            // Assert
            result.ShouldBeOfType<PaymentsListVm>();
            result.Payments.Count.ShouldBeGreaterThan(0);
        }

    }
}