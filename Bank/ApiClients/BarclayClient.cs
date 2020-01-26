using System;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Bank.ApiClients
{
    public class BarclayClient : IBankClient
    {
        public PaymentResult ProcessPayment(int merchantId, string cardHolderName, string cardNumber,
            string expiryYearMonthString, string cvv, decimal amount, string currency)
        {
            // TODO: Implement api client to Barclay bank
            throw new NotImplementedException();
        }
    }
}