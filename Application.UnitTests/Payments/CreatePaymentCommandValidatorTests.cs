using Application.Features.Payments.Commands.CreatePayment;
using FluentValidation.TestHelper;
using Xunit;

namespace Application.UnitTests.Payments
{
    public class CreatePaymentCommandValidatorTests
    {
        private readonly CreatePaymentCommandValidator validator;
        
        public CreatePaymentCommandValidatorTests()
        {
            validator = new CreatePaymentCommandValidator();
        }

        [Fact]
        public void MerchantId_ShouldBeNumber()
        {
            validator.ShouldHaveValidationErrorFor(x => x.MerchantId, 0);
            
            validator.ShouldNotHaveValidationErrorFor(x => x.MerchantId, 1);
        }

        [Fact]
        public void CardHolderName_ShouldNotBeEmpty()
        {
            validator.ShouldHaveValidationErrorFor(x => x.CardHolderName, "");

            validator.ShouldNotHaveValidationErrorFor(x => x.CardHolderName, "John Smith");
        }
        
        [Fact]
        public void CardHolderName_LengthShouldBeLessThanOrEqualTo60()
        {
            validator.ShouldHaveValidationErrorFor(x => x.CardHolderName, new string('A', 61));
            
            validator.ShouldNotHaveValidationErrorFor(x => x.CardHolderName, new string('A', 60));
        }

        [Fact]
        public void CardNumber_ShouldBeInCorrectFormat()
        {
            validator.ShouldHaveValidationErrorFor(x => x.CardNumber, "AAAA-BBBB");
            validator.ShouldHaveValidationErrorFor(x => x.CardNumber, "AAAA-BBBB-1111-2222");
            validator.ShouldHaveValidationErrorFor(x => x.CardNumber, "11112222333344");
            validator.ShouldHaveValidationErrorFor(x => x.CardNumber, "1111-2222-3333-44");
            
            validator.ShouldNotHaveValidationErrorFor(x => x.CardNumber, "1111222233334444");
            validator.ShouldNotHaveValidationErrorFor(x => x.CardNumber, "1111-2222-3333-4444");
        }
        
        [Fact]
        public void Cvv_ShouldThreeDigitNumber()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Cvv, "abc");
            validator.ShouldHaveValidationErrorFor(x => x.Cvv, "1bc");
            validator.ShouldHaveValidationErrorFor(x => x.Cvv, "12");
            validator.ShouldHaveValidationErrorFor(x => x.Cvv, "1234");
            validator.ShouldHaveValidationErrorFor(x => x.Cvv, "");

            validator.ShouldNotHaveValidationErrorFor(x => x.Cvv, "123");
        }
    }
}