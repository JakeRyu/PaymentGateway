using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest
    {
        public Guid MerchantId { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int Cvv { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        // Having a handler inside command makes it easy to follow logic, also improve discoverability 
        public class Handler : IRequestHandler<CreatePaymentCommand>
        {
            private readonly IApplicationDbContext _dbContext;
            private readonly IAcquireBank _acquireBank;

            public Handler(IApplicationDbContext dbContext, IAcquireBank acquireBank)
            {
                _dbContext = dbContext;
                _acquireBank = acquireBank;
            }
            
            public async Task<Unit> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
            {
                //todo: Use FluentValidator to validate input
                
                // Money is a good candidate for ValueObject
                // No requirement for Money to implement any logic in this simple scenario but wanted to point out that
                // it will help, for example, sort the amount considering currency.
                var amount = new Money(request.Amount, request.Currency);

                // Acquire bank
                var bankClient = _acquireBank.Create(request.CardNumber);
                
                var result = bankClient.ProcessPayment(request.MerchantId.ToString(), request.CardHolderName, 
                    request.CardNumber, request.ExpiryYear, request.ExpiryMonth, amount.Amount, amount.Currency);

                if (result.Status != "Success")
                {
                    throw new PaymentNotAcceptedException(result.Status);
                }
                
                await StorePaymentDetails(request, result.PaymentId, amount, cancellationToken);

                return Unit.Value;
            }

            private async Task StorePaymentDetails(CreatePaymentCommand request, Guid paymentId, Money amount, CancellationToken cancellationToken)
            {
                var entity = new Payment(paymentId, request.MerchantId, request.CardHolderName, request.CardNumber);

                _dbContext.Payments.Add(entity);
                
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}