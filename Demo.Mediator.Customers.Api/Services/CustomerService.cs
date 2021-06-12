using System.Threading.Tasks;
using AutoMapper;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.DataAccess.Commands;
using Demo.Mediator.Customers.Api.DataAccess.Queries;
using Demo.Mediator.Customers.Api.Models.Assets;
using Demo.Mediator.Customers.Api.Models.Requests;
using MediatR;

namespace Demo.Mediator.Customers.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CustomerService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public Task<Result> CreateCustomerAsync(CreateCustomerRequest request)
        {
            var command = _mapper.Map<CreateCustomerCommand>(request);
            return _mediator.Send(command);
        }

        public Task<Result<Customer>> GetCustomerAsync(GetCustomerByIdRequest request)
        {
            var query = _mapper.Map<GetCustomerByIdQuery>(request);
            return _mediator.Send(query);
        }
    }
}