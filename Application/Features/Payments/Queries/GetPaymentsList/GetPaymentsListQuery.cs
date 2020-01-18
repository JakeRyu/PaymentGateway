using System;
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
        public Guid MerchantId { get; set; }
        
        public class Handler : IRequestHandler<GetPaymentsListQuery, PaymentsListVm>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }
            
            public async Task<PaymentsListVm> Handle(GetPaymentsListQuery request, CancellationToken cancellationToken)
            {
                var payments = await _context.Payments
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