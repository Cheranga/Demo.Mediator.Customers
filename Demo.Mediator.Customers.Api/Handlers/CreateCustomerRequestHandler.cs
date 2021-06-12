using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.Models.Requests;
using MediatR;

namespace Demo.Mediator.Customers.Api.Handlers
{
    public class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, Result>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateCustomerRequestHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var customerSearchRequest = _mapper.Map<GetCustomerByIdRequest>(request);
            var operation = await _mediator.Send(customerSearchRequest, cancellationToken);

            if (!operation.Status)
            {
                return Result.Failure(operation.ErrorCode, operation.ValidationResult);
            }

            var customer = operation.Data;
            if (customer != null)
            {
                return Result.Failure(ErrorCodes.CustomerExists, ErrorMessages.CustomerExists);
            }

            var createCustomerOperation = await _mediator.Send(request, cancellationToken);
            return createCustomerOperation;
        }
    }
}