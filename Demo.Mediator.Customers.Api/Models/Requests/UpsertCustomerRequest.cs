using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.DataAccess.Commands;
using Demo.Mediator.Customers.Api.Extensions;
using MediatR;

namespace Demo.Mediator.Customers.Api.Models.Requests
{
    public class UpsertCustomerRequest : CommandBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class UpsertCustomerRequestHandler : IRequestHandler<UpsertCustomerRequest, Result>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpsertCustomerRequestHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public Task<Result> Handle(UpsertCustomerRequest request, CancellationToken cancellationToken)
        {
            var customerId = request.Id;
            if (customerId.IsEmpty())
            {
                return CreateCustomerAsync(request, cancellationToken);
            }

            return UpdateCustomerAsync(request, cancellationToken);
        }

        private async Task<Result> UpdateCustomerAsync(UpsertCustomerRequest request, CancellationToken cancellationToken)
        {
            var searchRequest = _mapper.Map<GetCustomerByIdRequest>(request);
            var searchOperation = await _mediator.Send(searchRequest, cancellationToken);

            if (!searchOperation.Status)
            {
                return Result.Failure(searchOperation.ErrorCode, searchOperation.ValidationResult);
            }

            var command = _mapper.Map<UpdateCustomerCommand>(request);
            var updateOperation = await _mediator.Send(command, cancellationToken);
            return updateOperation;
        }

        private async Task<Result> CreateCustomerAsync(UpsertCustomerRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateCustomerCommand>(request);
            var createOperation = await _mediator.Send(command, cancellationToken);
            return createOperation;
        }
    }
}