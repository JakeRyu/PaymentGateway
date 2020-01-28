using System;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Payments.Queries.GetPaymentsList
{
    public class PaymentDto : IMapFrom<Payment>
    {
        public Guid Id { get; set; }
        public int MerchantId { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryYearMonth { get; set; }
        public string Cvv { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Payment, PaymentDto>()
                .ForMember(dest => dest.ExpiryYearMonth,
                    opt => opt.MapFrom(
                        src => src.CardExpiryDate.ToString()))
                .ForMember(dest => dest.Amount,
                    opt => opt.MapFrom(
                        src => src.Money.Amount))
                .ForMember(dest => dest.Currency,
                    opt => opt.MapFrom(
                        src => src.Money.Currency))
                .ForMember(dest => dest.CardNumber,
                    opt => opt.MapFrom(
                        src => src.CardNumber.Value));    // assign masked value
        }
    }
}