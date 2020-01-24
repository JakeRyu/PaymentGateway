using System;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Bank.ApiClients
{
    public class SantanderClient : IBankClient
    {
        public PaymentResult ProcessPayment(int merchantId, string cardHolderName, string cardNumber, string expiryYear,
            string expiryMonth, string cvv, decimal amount, string currency)
        {
            // TODO: Implement api client to Santander bank
            throw new NotImplementedException();
        }
    }
}