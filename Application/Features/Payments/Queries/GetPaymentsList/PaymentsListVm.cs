using System.Collections.Generic;
using AutoMapper;

namespace Application.Features.Payments.Queries.GetPaymentsList
{
    public class PaymentsListVm
    {
        public IList<PaymentDto> Payments { get; set; }
    }
}