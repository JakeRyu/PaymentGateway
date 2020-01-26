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
        public CardExpiryDate CardExpiryDate { get; private set; }    // Value object
        public string Cvv { get; private set; }
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        public Payment()
        {
        }

        public Payment(Guid id, int merchantId, string cardHolderName, string cardNumber, string expiryYearMonthString, string cvv, decimal amount, string currency)
        {
            Id = id;
            MerchantId = merchantId;
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            CardExpiryDate = CardExpiryDate.For(expiryYearMonthString);
            Cvv = cvv;
            Amount = amount;
            Currency = currency;
        }
    }
}