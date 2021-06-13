using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.DataAccess.Queries;
using Demo.Mediator.Customers.Api.Models.Responses;
using MediatR;

namespace Demo.Mediator.Customers.Api.Models.Requests
{
    public class GetCustomerByUserNameRequest : QueryBase<GetCustomerResponse>
    {
        public string UserName { get; set; }
    }
    
    public class GetCustomerByUserNameRequestHandler : IRequestHandler<GetCustomerByUserNameRequest, Result<GetCustomerResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetCustomerByUserNameRequestHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        public async Task<Result<GetCustomerResponse>> Handle(GetCustomerByUserNameRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetCustomerByUserNameQuery>(request);
            var operation = await _mediator.Send(query, cancellationToken);

            if (!operation.Status)
            {
                return Result<GetCustomerResponse>.Failure(operation.ErrorCode, operation.ValidationResult);
            }

            var customer = operation.Data;
            if (customer == null)
            {
                return Result<GetCustomerResponse>.Failure(ErrorCodes.CustomerNotFound, ErrorMessages.CustomerNotFound);
            }
            
            var response = _mapper.Map<GetCustomerResponse>(operation.Data);
            return Result<GetCustomerResponse>.Success(response);
        }
    }
}