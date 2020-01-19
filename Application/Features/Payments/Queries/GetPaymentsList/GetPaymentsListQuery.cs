using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Payments.Queries.GetPaymentsList
{
    public class GetPaymentsListQuery : IRequest<PaymentsListVm>
    {
        public int MerchantId { get; set; }
        
        public class Handler : IRequestHandler<GetPaymentsListQuery, PaymentsListVm>
        {
            private readonly IApplicationDbContext _dbContext;

            public Handler(IApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
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