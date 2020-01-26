using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Application.Features.Payments.Queries.GetPaymentsList
{
    public class GetPaymentsListQuery : IRequest<PaymentsListVm>
    {
        public int MerchantId { get; set; }
        
        public class Handler : IRequestHandler<GetPaymentsListQuery, PaymentsListVm>
        {
            private readonly IApplicationDbContext _dbContext;
            private readonly IMapper _mapper;
            private static readonly ILogger _logger = Log.Logger;

            public Handler(IApplicationDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            
            public async Task<PaymentsListVm> Handle(GetPaymentsListQuery request, CancellationToken cancellationToken)
            {
                var payments = await _dbContext.Payments
                    .AsNoTracking()
                    .Where(x => x.MerchantId == request.MerchantId)
                    .ProjectTo<PaymentDto>(_mapper.ConfigurationProvider)
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