using System;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Infrastructure.Bank
{
    public class ShopperBank : IBank
    {
        /// <summary>
        /// For the sake of simulation, it only accepts the sample card number "1111222233334444".
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="expiryYear"></param>
        /// <param name="expiryMonth"></param>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public PaymentResult ProcessPayment(string cardNumber, int expiryYear, int expiryMonth, decimal amount, string currency)
        {
            if (cardNumber != "1111222233334444")
            {
                return new PaymentResult
                {
                    Status = "Fail"
                };
            }

            return new PaymentResult
            {
                PaymentId = Guid.NewGuid(),
                Status = "Success"    // todo: standardise payment result status
            };
        }
    }
}