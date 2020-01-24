using System;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public int MerchantId { get; private set; }
        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public int ExpiryMonth { get; private set; }
        public int ExpiryYear { get; private set; }
        public int Cvv { get; private set; }
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        public Payment()
        {
        }

        public Payment(int merchantId, string cardHolderName, string cardNumber, int expiryMonth, 
            int expiryYear, int cvv, decimal amount, string currency)
        {
            MerchantId = merchantId;
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            Cvv = cvv;
            Amount = amount;
            Currency = currency;
        }
    }
}