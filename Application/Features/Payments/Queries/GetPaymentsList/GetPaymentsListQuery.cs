using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Features.Payments.Queries.GetPaymentsList
{
    public class GetPaymentsListQuery : IRequest<PaymentsListVm>
    {
        public int MerchantId { get; set; }
        
        public class Handler : IRequestHandler<GetPaymentsListQuery, PaymentsListVm>
        {
            private readonly IApplicationDbContext _dbContext;
            private readonly ILogger<Handler> _logger;

            public Handler(IApplicationDbContext dbContext, ILogger<Handler> logger)
            {
                _dbContext = dbContext;
                _logger = logger;
            }
            
            public async Task<PaymentsListVm> Handle(GetPaymentsListQuery request, CancellationToken cancellationToken)
            {
                var payments = await _dbContext.Payments
                    .Where(x => x.MerchantId == request.MerchantId)
                    .ToListAsync(cancellationToken);
        
                var vm = new PaymentsListVm
                {
                    Payments = payments
                };
        
                return vm;
            }
        }
    }
}