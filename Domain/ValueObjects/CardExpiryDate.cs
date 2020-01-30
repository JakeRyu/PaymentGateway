using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Exceptions;
using Serilog;

namespace Domain.ValueObjects
{
    public class CardExpiryDate : ValueObject
    {
        private static readonly ILogger _logger = Log.Logger;
        public DateTime Date { get; private set; }

        private CardExpiryDate()
        {
        }

        /// <summary>
        /// Produce a card expiry date based on month and year
        /// </summary>
        /// <param name="expiryMonthYearString">mm/yy format</param>
        /// <returns></returns>
        /// <exception cref="CardExpiryDateInvalidException"></exception>
        public static CardExpiryDate For(string expiryMonthYearString)
        {
            var cardExpiryDate = new CardExpiryDate();
            
            try
            {
                var index = expiryMonthYearString.IndexOf("/", StringComparison.Ordinal);
                var monthString = expiryMonthYearString.Substring(0, index);
                var twoDigitYearString = expiryMonthYearString.Substring(index + 1);

                var month = int.Parse(monthString);
                var twoDigitYear = int.Parse(twoDigitYearString);
                var fourDigitYear = twoDigitYear >= 50 ? 1900 + twoDigitYear : 2000 + twoDigitYear;

                var firstDayOfMonth = new DateTime(fourDigitYear, month, 1);
                cardExpiryDate.Date = firstDayOfMonth.AddMonths(1).AddDays(-1);
                
                _logger.Debug("Card expiry date {Date} converted from '{String}'", 
                    cardExpiryDate.Date.ToShortDateString(), expiryMonthYearString);
            }
            catch(Exception ex)
            {
                throw new CardExpiryDateInvalidException(expiryMonthYearString, ex);
            }
            
            return cardExpiryDate;
        }

        public static implicit operator string(CardExpiryDate cardExpiryDate)
        {
            return cardExpiryDate.ToString();
        }

        public override string ToString()
        {
            var year = Date.Year.ToString().Substring(2);
            var month = Date.Month >= 10 ? Date.Month.ToString() : "0" + Date.Month;

            return $"{month}/{year}";
        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Date;
        }
    }
}