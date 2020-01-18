using System;

namespace Application.Common.Exceptions
{
    public class PaymentNotAcceptedException : Exception
    {
        public PaymentNotAcceptedException(string status)
            : base($"Payment request was not accepted with status '{status}'")
        {
        }
    }
}