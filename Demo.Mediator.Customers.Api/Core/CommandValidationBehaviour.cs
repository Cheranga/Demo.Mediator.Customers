using System.Threading;
using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Constants;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Demo.Mediator.Customers.Api.Core
{
    public class CommandValidationBehaviour<TRequest> : IPipelineBehavior<TRequest, Result>
    {
        private readonly ILogger<CommandValidationBehaviour<TRequest>> _logger;
        private readonly IValidator<TRequest> _validator;

        public CommandValidationBehaviour(ILogger<CommandValidationBehaviour<TRequest>> logger, IValidator<TRequest> validator = null)
        {
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result> next)
        {
            if (_validator == null)
            {
                return await next();
            }

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Error occurred when validating {RequestType}\n{ValidationErrors}", typeof(TRequest).Name, validationResult);
                return Result.Failure(ErrorCodes.InvalidInput, validationResult);
            }

            _logger.LogInformation("Validation successful for {RequestType}", typeof(TRequest).Name);
            return await next();
        }
    }
}