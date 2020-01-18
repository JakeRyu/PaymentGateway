using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IBankClient
    {
        PaymentResult ProcessPayment(string merchantId, string cardHolderName, string cardNumber, int expiryYear, int expiryMonth, decimal amount, string currency);
    }
}