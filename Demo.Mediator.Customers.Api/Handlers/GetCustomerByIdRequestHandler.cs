using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.DataAccess.Queries;
using Demo.Mediator.Customers.Api.Models.Assets;
using Demo.Mediator.Customers.Api.Models.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Demo.Mediator.Customers.Api.Handlers
{
    public class GetCustomerByIdRequestHandler : IRequestHandler<GetCustomerByIdRequest, Result<Customer>>
    {
        private readonly ILogger<GetCustomerByIdRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetCustomerByIdRequestHandler(IMediator mediator, IMapper mapper, ILogger<GetCustomerByIdRequestHandler> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<Customer>> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetCustomerByIdQuery>(request);
            var operation = await _mediator.Send(query, cancellationToken);

            if (!operation.Status)
            {
                _logger.LogError(operation.ErrorCode, operation.ValidationResult);
            }

            var customer = operation.Data;
            if (customer == null)
            {
                return Result<Customer>.Failure(ErrorCodes.CustomerNotFound, ErrorMessages.CustomerNotFound);
            }

            return Result<Customer>.Success(customer);
        }
    }
}