using System;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Bank.ApiClients
{
    public class SantanderClient : IBankClient
    {
        public PaymentResult ProcessPayment(string merchantId, string cardHolderName, string cardNumber, int expiryYear, 
            int expiryMonth, decimal amount, string currency)
        {
            // TODO: Implement api client to Santander bank
            throw new NotImplementedException();
        }
    }
}