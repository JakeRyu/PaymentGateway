using System.Threading.Tasks;
using Application.Features.Payments.Commands.CreatePayment;
using Application.Features.Payments.Queries.GetPaymentsList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private static readonly ILogger _logger = Log.Logger;
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{merchantId}")]
        public async Task<IActionResult> GetPaymentsList(int merchantId)
        {
            _logger.Debug("[API] Get payments list by merchant id: {Merchant}", merchantId);
            
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
            _logger.Debug("[API] Create payment with {@CreatePaymentCommand}}", command);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}