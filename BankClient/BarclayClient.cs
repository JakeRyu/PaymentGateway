using System;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace BankClient
{
    public class BarclayClient : IBankClient
    {
        public PaymentResult ProcessPayment(string merchantId, string cardHolderName, string cardNumber, int expiryYear, 
            int expiryMonth, decimal amount, string currency)
        {
            // Assume that an api call to a bank is made and has gone through successfully whereas PaymentResult object gets created
            // based on the response from the bank api.

            return new PaymentResult
            {
                Status = "Success",
                PaymentId = Guid.NewGuid()
            };

            // In the test, this method can be mocked to simulate error response.
        }
    }
}