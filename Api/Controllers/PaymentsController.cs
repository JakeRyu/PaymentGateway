using System.Threading.Tasks;
using Application.Features.Payments.Commands.CreatePayment;
using Application.Features.Payments.Queries.GetPaymentsList;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{merchantId}")]
        public async Task<IActionResult> GetPaymentsList(int merchantId)
        {
            _logger.LogInformation("GetPaymentList api called");
            
            var vm = await _mediator.Send(new GetPaymentsListQuery
            {
                MerchantId = merchantId
            });
        
            return Ok(vm);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]public async Task<IActionResult> CreatePayment([FromBody]CreatePaymentCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}