using System;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Payment : AuditableEntity
    {
        public Guid Id { get; private set; }
        public int MerchantId { get; private set; }
        public string CardHolderName { get; private set; }
        public MaskedString CardNumber { get; private set; }    // Value object
        public CardExpiryDate CardExpiryDate { get; private set; }    // Value object
        public string Cvv { get; private set; }
        public Money Money { get; private set; }    // Value object

        
        public Payment()
        {
        }

        public Payment(Guid id, int merchantId, string cardHolderName, string cardNumber, string expiryYearMonthString, string cvv, decimal amount, string currency)
        {
            Id = id;
            MerchantId = merchantId;
            CardHolderName = cardHolderName;
            CardNumber = new MaskedString(cardNumber);
            CardExpiryDate = CardExpiryDate.For(expiryYearMonthString);
            Cvv = cvv;
            Money = new Money(amount, currency);
        }

        // This is one of possible scenarios, not from requirements.
        // To demonstrate LastModifiedOn property is updated accordingly. 
        public void UpdateCardHolderName(string cardHolderName)
        {
            CardHolderName = cardHolderName;
        }
    }
}