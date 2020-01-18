using Application.Common.Interfaces;
using Application.Common.Models;

namespace BankClient
{
    /// <summary>
    /// BankClientFactory uses this class when it couldn't identify a bank.
    /// This way ensures that BankClientFactory class always return a kind of bank client without checking further. 
    /// </summary>
    public class NullClient : IBankClient
    {
        public PaymentResult ProcessPayment(string merchantId, string cardHolderName, string cardNumber, int expiryYear, 
            int expiryMonth, decimal amount, string currency)
        {
            return new PaymentResult
            {
                Status = "Unknown bank"
            };
        }
    }
}