using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.MerchantId).NotEmpty();
            RuleFor(x => x.CardHolderName).MaximumLength(60).NotEmpty();
            RuleFor(x => x.CardNumber).CreditCard();
            // todo: 01 - 12 Use RegEx for format mm/yy and month range 
            RuleFor(x => x.ExpiryYearMonthString).NotEmpty();
            RuleFor(x => x.Cvv).Matches(@"\b[0-9]{3}\b");
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.Currency).NotEmpty();
            
        }
    }
}