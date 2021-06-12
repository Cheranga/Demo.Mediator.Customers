using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Demo.Mediator.Customers.Api.Core
{
    public class CommandPerformanceBehaviour<TRequest> : IPipelineBehavior<TRequest, Result>
    {
        private readonly ILogger<CommandPerformanceBehaviour<TRequest>> _logger;

        public CommandPerformanceBehaviour(ILogger<CommandPerformanceBehaviour<TRequest>> logger)
        {
            _logger = logger;
        }

        public async Task<Result> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result> next)
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