using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.Payments.Queries.GetPaymentsList;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Application.Features.Payments.Queries.GetPaymentDetails
{
    public class GetPaymentDetailsQuery : IRequest<PaymentDto>
    {
        public Guid PaymentId { get; set; }

        public class Handler : IRequestHandler<GetPaymentDetailsQuery, PaymentDto>
        {
            private readonly IApplicationDbContext _dbContext;
            private readonly IMapper _mapper;
            private static readonly ILogger _logger = Log.Logger;

            public Handler(IApplicationDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            
            public async Task<PaymentDto> Handle(GetPaymentDetailsQuery request, CancellationToken cancellationToken)
            {
                var result = await _dbContext.Payments
                    .AsNoTracking()
                    .Where(x => x.Id == request.PaymentId)
                    .ProjectTo<PaymentDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);
                
                if(result == null)
                    throw new EntityNotFoundException("Payment", request.PaymentId);

                return result;
            }
        }
    }
}