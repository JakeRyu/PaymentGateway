using System;

namespace Domain.Exceptions
{
    public class CardExpiryDateInvalidException : Exception
    {
        public CardExpiryDateInvalidException(string expiryMonthYearString, Exception ex) 
            : base($"Month and year \"{expiryMonthYearString}\" is invalid.", ex)
        {
        }
    }
}