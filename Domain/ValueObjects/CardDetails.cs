using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public class CardDetails : ValueObject
    {
        public string CardNumber { get; private set; }
        public int ExpiryMonth { get; private set; }
        public int ExpiryYear { get; private set; }
        public int Cvv { get; private set; }

        private CardDetails()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNumber">Assumes that the card number may be accepted with spaces or dashes</param>
        /// <param name="expiryMonthYearString">Assumes this is a string in a form of MM/YY</param>
        /// <param name="cvv"></param>
        /// <returns></returns>
        public static CardDetails For(string cardNumber, string expiryMonthYearString, int cvv)
        {
            var cardDetails = new CardDetails();

            // todo: validate card number
            cardDetails.CardNumber = cardNumber;
            
            try
            {
                var index = expiryMonthYearString.IndexOf("/", StringComparison.Ordinal);
                var monthString = expiryMonthYearString.Substring(0, index);
                var yearString = expiryMonthYearString.Substring(index + 1);
                
                int.TryParse(monthString, out var month);
                int.TryParse(yearString, out var twoDigitYear);

                cardDetails.ExpiryMonth = month;
                cardDetails.ExpiryYear = twoDigitYear >= 50 ? twoDigitYear + 1900 : twoDigitYear + 2000;
            }
            catch(Exception ex)
            {
                throw new CardExpiryDateInvalidException(expiryMonthYearString, ex);
            }
            
            // todo: validate cvv
            cardDetails.Cvv = cvv;
            
            return cardDetails;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CardNumber;
            yield return ExpiryMonth;
            yield return ExpiryYear;
        }
    }
}