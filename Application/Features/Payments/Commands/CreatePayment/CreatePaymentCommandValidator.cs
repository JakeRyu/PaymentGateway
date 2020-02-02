using System.Linq;
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
            // RuleFor(x => x.ExpiryYearMonthString).NotEmpty().Must(str =>
            // {
            //     var regex = new Regex(@"\b[0-1][0-9]/[0-9]{2}\b");
            //     return regex.IsMatch(str);
            // }).WithMessage("Card expiration date should be mm/yy format as same as the physical card");
            RuleFor(x => x.Cvv).Matches(@"\b[0-9]{3}\b");
            // RuleFor(x => x.Amount).NotEmpty().Must(amount => amount > 0);
            // RuleFor(x => x.Currency).NotEmpty().Must(c =>
            // {
            //     var availableCurrencies = new[] {"GBP", "EUR", "USD"};
            //     return availableCurrencies.Contains(c.ToUpper());
            // }).WithMessage("Available currencies are GBP, EUR, USD");
        }
    }
}