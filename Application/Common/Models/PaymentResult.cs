using System;

namespace Application.Common.Models
{
    public class PaymentResult
    {
        public Guid PaymentId { get; set; }
        public string Status { get; set; }
    }
}