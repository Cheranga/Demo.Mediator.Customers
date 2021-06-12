using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Demo.Mediator.Customers.Api.Core
{
    public class QueryPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
    {
        private readonly ILogger<QueryPerformanceBehaviour<TRequest, TResponse>> _logger;

        public QueryPerformanceBehaviour(ILogger<QueryPerformanceBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<TResponse>> next)
        {
            _logger.LogInformation("Started handling {RequestType}", typeof(TRequest).Name);
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var operation = await next();
            stopWatch.Stop();
            _logger.LogInformation("Finished handling {RequestType}. Time taken {TimeTaken}ms", typeof(TRequest).Name, stopWatch.ElapsedMilliseconds);

            return operation;
        }
    }
}