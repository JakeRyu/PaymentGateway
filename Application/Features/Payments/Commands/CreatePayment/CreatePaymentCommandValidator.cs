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
            RuleFor(x => x.ExpiryYearMonthString).NotEmpty().Must(str =>
            {
                if (string.IsNullOrEmpty(str)) return false;
                
                var regex = new Regex(@"\b[0-1][0-9]/[0-9]{2}\b");
                if (regex.IsMatch(str))
                {
                    var month = int.Parse(str.Substring(0, 2));
                    return month >= 1 && month <= 12;
                }

                return false;
            }).WithMessage("Card expiration date should be mm/yy format as same as the physical card");
            RuleFor(x => x.Cvv).Matches(@"\b[0-9]{3}\b");
            RuleFor(x => x.Amount).NotEmpty().Must(amount => amount > 0);
            RuleFor(x => x.Currency).NotEmpty().Must(cur =>
            {
                cur = cur?.ToUpper();
                var availableCurrencies = new[] {"GBP", "EUR", "USD"};
                return availableCurrencies.Contains(cur);
            }).WithMessage("Available currencies are GBP, EUR, USD");
        }
    }
}