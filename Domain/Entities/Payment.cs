using System;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public Guid MerchantId { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        // public Money Amount { get; set; }

        public Payment()
        {
            
        }
        public Payment(Guid paymentId, Guid merchantId, string cardHolderName, string cardNumber, Money amount)
        {
            PaymentId = paymentId;
            MerchantId = merchantId;
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            // Amount = amount;
        }
    }
}