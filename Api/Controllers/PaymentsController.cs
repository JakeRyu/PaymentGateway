using System.Threading.Tasks;
using Application.Features.Payments.Commands.CreatePayment;
using Application.Features.Payments.Queries.GetPaymentsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly IMediator _mediator;

        public PaymentsController(ILogger<PaymentsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentsList(int merchantId)
        {
            var vm = _mediator.Send(new GetPaymentsListQuery
            {
                MerchantId = merchantId
            });
        
            return Ok(vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreatePayment()
        {
            await _mediator.Send(new CreatePaymentCommand());

            return NoContent();
        }
    }
}