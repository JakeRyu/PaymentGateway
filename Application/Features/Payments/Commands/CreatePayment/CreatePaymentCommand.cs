using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using Serilog;

namespace Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<Guid>
    {
        public int MerchantId { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string Cvv { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        // Having a handler inside command makes it easy to follow logic, also improve discoverability 
        public class Handler : IRequestHandler<CreatePaymentCommand, Guid>
        {
            private readonly IApplicationDbContext _dbContext;
            private readonly IAcquireBank _acquireBank;
            private static readonly ILogger _logger = Log.Logger;

            public Handler(IApplicationDbContext dbContext, IAcquireBank acquireBank)
            {
                _dbContext = dbContext;
                _acquireBank = acquireBank;
            }
            
            public async Task<Guid> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
            {
                // Acquire bank
                var bankClient = _acquireBank.Create(request.CardNumber);
                
                var result = bankClient.ProcessPayment(request.MerchantId, request.CardHolderName, 
                    request.CardNumber, request.ExpiryYear, request.ExpiryMonth, request.Cvv, request.Amount, request.Currency);

                if (result.Status != "Success")
                {
                    _logger.Error("Payment request was not accepted for reason: {Status}", result.Status);
                    throw new PaymentNotAcceptedException(result.Status);
                }
                
                var paymentId = await StorePaymentDetails(request, result.PaymentId, cancellationToken);

                return paymentId;
            }

            private async Task<Guid> StorePaymentDetails(CreatePaymentCommand request, Guid paymentId, CancellationToken cancellationToken)
            {
                var entity = new Payment(
                    paymentId,
                    request.MerchantId, 
                    request.CardHolderName, 
                    request.CardNumber,
                    request.ExpiryMonth,
                    request.Cvv,
                    request.Amount,
                    request.Currency);

                _dbContext.Payments.Add(entity);
                
                await _dbContext.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}