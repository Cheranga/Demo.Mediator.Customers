using System.Threading;
using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Constants;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Demo.Mediator.Customers.Api.Core
{
    public class QueryValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
    {
        private readonly IValidator<TRequest> _validator;
        private readonly ILogger<QueryValidationBehaviour<TRequest, TResponse>> _logger;

        public QueryValidationBehaviour(ILogger<QueryValidationBehaviour<TRequest, TResponse>> logger, IValidator<TRequest> validator = null)
        {
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<TResponse>> next)
        {
            if (_validator == null)
            {
                return await next();
            }

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Error occurred when validating {RequestType}\n{ValidationErrors}", typeof(TRequest).Name, validationResult);
                return Result<TResponse>.Failure(ErrorCodes.InvalidInput, validationResult);
            }

            _logger.LogInformation("Validation successful for {RequestType}", typeof(TRequest).Name);
            return await next();
        }
    }
}