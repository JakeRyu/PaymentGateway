using System.Threading.Tasks;
using Application.Features.Payments.Commands.CreatePayment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IMediator _mediator;

        public PaymentController(ILogger<PaymentController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // [HttpGet]
        // public async Task<IActionResult> GetPayments(string merchantId)
        // {
        //     var vm = _mediator.Send(new GetPaymentsQuery(merchantId));
        //
        //     return Ok(vm);
        // }
        
        [HttpPost]
        public async Task<IActionResult> CreatePayment()
        {
            await _mediator.Send(new CreatePaymentCommand());

            return NoContent();
        }
    }
}