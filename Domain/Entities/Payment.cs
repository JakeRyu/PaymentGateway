using System;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public int MerchantId { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        // public Money Amount { get; set; }

        private Payment()
        {
            
        }
        
        //TODO: Complete parameter list
        public Payment(Guid id, int merchantId, string cardHolderName, string cardNumber)
        {
            Id = id;
            MerchantId = merchantId;
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            // Amount = amount;
        }
    }
}