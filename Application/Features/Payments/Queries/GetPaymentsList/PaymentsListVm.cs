using System.Collections.Generic;
using Domain.Entities;

namespace Application.Features.Payments.Queries.GetPaymentsList
{
    public class PaymentsListVm
    {
        public IList<Payment> Payments { get; set; }
    }
}