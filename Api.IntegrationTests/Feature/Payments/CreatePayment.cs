using System.Net;
using System.Threading.Tasks;
using Api.IntegrationTests.Common;
using Application.Features.Payments.Commands.CreatePayment;
using Shouldly;
using Xunit;

namespace Api.IntegrationTests.Feature.Payments
{
    public class CreatePayment : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CreatePayment(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact(Skip="To debug")]
        public async Task GivenCreatePaymentCommand_ShouldReturnNewPaymentId()
        {
            var client = _factory.CreateClient();
            
            var command = new CreatePaymentCommand
            {
                MerchantId = 1,
                CardHolderName = "Jake Ryu",
                CardNumber = "1111222233334444",
                ExpiryYearMonthString = "05/22",
                Cvv = "989",
                Amount = 24,
                Currency = "GBP"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync("/payments", content);

            response.EnsureSuccessStatusCode();
            
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
        }
        
        
    }
}