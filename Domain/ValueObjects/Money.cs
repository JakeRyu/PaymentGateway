using System.Collections.Generic;
using Domain.Common;

namespace Domain.ValueObjects
{
    public class Money : ValueObject
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}