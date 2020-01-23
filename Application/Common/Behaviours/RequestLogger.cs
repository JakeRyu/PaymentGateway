using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    /// <summary>
    /// Before process a request, log which request (command or query) has come through.
    /// </summary>
    /// <typeparam name="TRequest">MediatR request to Application layer</typeparam>
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<TRequest> _logger;

        public RequestLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;
            
            _logger.LogInformation("[PG Request] {Name} {@Request}",
                name, request);

            return Task.CompletedTask;
        }
    }
}