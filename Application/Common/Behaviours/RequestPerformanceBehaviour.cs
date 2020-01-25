using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace Application.Common.Behaviours
{
    /// <summary>
    /// Log warning if any request takes longer than half second.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private static readonly ILogger _logger = Log.Logger;

        public RequestPerformanceBehaviour()
        {
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                _timer.Start();

                var response = await next();

                _timer.Stop();

                if (_timer.ElapsedMilliseconds > 500)
                {
                    var name = typeof(TRequest).Name;

                    _logger.Warning(
                        "[Request Performance] {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                        name, _timer.ElapsedMilliseconds, request);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "[Application Exception] {Message}", ex.Message);
                throw;
            }
        }
    }
}