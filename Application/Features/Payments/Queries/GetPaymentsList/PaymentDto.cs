using System;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Payments.Queries.GetPaymentsList
{
    public class PaymentDto : IMapFrom<Payment>
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string Cvv { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}