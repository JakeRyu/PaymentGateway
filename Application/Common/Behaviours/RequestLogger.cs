using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Serilog;

namespace Application.Common.Behaviours
{
    /// <summary>
    /// Before process a request, log which request (command or query) has come through.
    /// </summary>
    /// <typeparam name="TRequest">MediatR request into Application layer</typeparam>
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private static readonly ILogger _logger = Log.Logger;

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;
            
            _logger.Debug("[Request Logger] {Name} {@Request}", name, request);

            return Task.CompletedTask;
        }
    }
}