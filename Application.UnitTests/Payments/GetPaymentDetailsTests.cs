using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.Payments.Queries.GetPaymentDetails;
using Application.Features.Payments.Queries.GetPaymentsList;
using AutoMapper;
using Microsoft.Extensions.Localization;
using Shouldly;
using Xunit;
using TestCommon;

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
            result.Id.ShouldBe(Constants.Sample1);
            result.MerchantId.ShouldBe(1);
            result.CardHolderName.ShouldBe("John Smith");
            result.CardNumber.ShouldNotBe("1111222233334444");
            result.ExpiryYearMonth.ShouldBe("05/20");
            result.Cvv.ShouldBe("298");
            result.Amount.ShouldBe(200m);
            result.Currency.ShouldBe("GBP");
        }
        
        [Fact]
        public async Task Handler_GivenNonExistingId_ShouldThrowEntityNotFoundException()
        {
            var query = new GetPaymentDetailsQuery
            {
                PaymentId = Guid.NewGuid()
            };
            var sut = new GetPaymentDetailsQuery.Handler(DbContext, Mapper);

            await Should.ThrowAsync<EntityNotFoundException>(async () => 
                await sut.Handle(query, CancellationToken.None));
        }
    }
}