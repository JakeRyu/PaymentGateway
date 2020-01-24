using FluentValidation;

namespace Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.MerchantId).NotEmpty();
            RuleFor(x => x.CardHolderName).MaximumLength(60).NotEmpty();
            RuleFor(x => x.CardNumber).MaximumLength(20).NotEmpty();
            RuleFor(x => x.ExpiryMonth).NotEmpty();
            RuleFor(x => x.ExpiryYear).NotEmpty();
            RuleFor(x => x.Cvv).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.Currency).NotEmpty();
            
        }
    }
}