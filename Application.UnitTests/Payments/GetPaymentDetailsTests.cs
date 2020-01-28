using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Features.Payments.Queries.GetPaymentDetails;
using Application.Features.Payments.Queries.GetPaymentsList;
using Application.UnitTests.Common;
using AutoMapper;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Payments
{
    [Collection("Application test collection")]
    public class GetPaymentDetailsTests
    {
        private IApplicationDbContext DbContext { get; }
        private IMapper Mapper { get; }

        public GetPaymentDetailsTests(TestFixture testFixture)
        {
            DbContext = testFixture.DbContext;
            Mapper = testFixture.Mapper;
        }

        [Fact]
        public async Task Handler_GivenValidRequest_ShouldReturnPaymentDetails()
        {
            var query = new GetPaymentDetailsQuery
            {
                PaymentId = Constants.Sample1
            };
            var sut = new GetPaymentDetailsQuery.Handler(DbContext, Mapper);

            var result = await sut.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<PaymentDto>();
            result.CardHolderName.ShouldBe("John Smith");
        }
    }
}