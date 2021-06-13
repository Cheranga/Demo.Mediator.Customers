using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.DataAccess.Commands;
using Demo.Mediator.Customers.Api.DataAccess.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Demo.Mediator.Customers.Api.Models.Requests
{
    public class DeleteCustomerRequest : CommandBase
    {
        public string CustomerId { get; set; }
    }
    
    public class DeleteCustomerRequestHandler : IRequestHandler<DeleteCustomerRequest, Result>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DeleteCustomerRequestHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        public async Task<Result> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetCustomerByIdRequest>(request);
            var searchOperation = await _mediator.Send(query, cancellationToken);

            if (!searchOperation.Status)
            {
                return Result.Failure(searchOperation.ErrorCode, searchOperation.ValidationResult);
            }

            var deleteCommand = _mapper.Map<DeleteCustomerCommand>(request);
            var deleteOperation = await _mediator.Send(deleteCommand, cancellationToken);
            return deleteOperation;
        }
    }
}