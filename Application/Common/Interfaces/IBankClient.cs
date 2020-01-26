using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IBankClient
    {
        PaymentResult ProcessPayment(int merchantId, string cardHolderName, string cardNumber,
            string expiryYearMonthString, string cvv, decimal amount, string currency);
    }
}