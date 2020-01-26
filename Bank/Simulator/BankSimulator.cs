using System;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Bank.Simulator
{
    public class BankSimulator : IBankClient
    {
        public PaymentResult ProcessPayment(int merchantId, string cardHolderName, string cardNumber,
            string expiryYearMonthString, string cvv, decimal amount, string currency)
        {
            // Validation for simulation purpose
            if (cardNumber != "1111222233334444")
                return new PaymentResult
                {
                    Status = "Card number is not valid"
                };

            return new PaymentResult
            {
                PaymentId = Guid.NewGuid(),
                Status = "Success"
            };
        }
    }
}