using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<int>
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
        public class Handler : IRequestHandler<CreatePaymentCommand, int>
        {
            private readonly IApplicationDbContext _dbContext;
            private readonly IAcquireBank _acquireBank;

            public Handler(IApplicationDbContext dbContext, IAcquireBank acquireBank)
            {
                _dbContext = dbContext;
                _acquireBank = acquireBank;
            }
            
            public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
            {
                // Acquire bank
                var bankClient = _acquireBank.Create(request.CardNumber);
                
                var result = bankClient.ProcessPayment(request.MerchantId, request.CardHolderName, 
                    request.CardNumber, request.ExpiryYear, request.ExpiryMonth, request.Cvv, request.Amount, request.Currency);

                if (result.Status != "Success")
                {
                    throw new PaymentNotAcceptedException(result.Status);
                }
                
                var paymentId = await StorePaymentDetails(request, cancellationToken);

                return paymentId;
            }

            private async Task<int> StorePaymentDetails(CreatePaymentCommand request, CancellationToken cancellationToken)
            {
                var entity = new Payment(
                    request.MerchantId, 
                    request.CardHolderName, 
                    request.CardNumber,
                    request.ExpiryMonth,
                    request.ExpiryYear,
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