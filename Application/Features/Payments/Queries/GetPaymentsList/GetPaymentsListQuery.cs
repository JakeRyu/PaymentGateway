using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Payments.Queries.GetPaymentsList
{
    public class GetPaymentsListQuery : IRequest<PaymentsListVm>
    {
        // public Guid MerchantId { get; set; }
        
        // public class Handler : IRequestHandler<GetPaymentsListQuery, PaymentsListVm>
        // {
        //     public async Task<PaymentsListVm> Handle(GetPaymentsListQuery request, CancellationToken cancellationToken)
        //     {
        //         var payments = await _context.Payments.Where(x => x.MerchantId == request.MerchantId);
        //
        //         var vm = new PaymentsListVm
        //         {
        //             Payments = payments
        //         };
        //
        //         return vm;
        //     }
        // }
    }
}