using System.Threading.Tasks;
using AutoMapper;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.Models.Requests;
using Demo.Mediator.Customers.Api.Models.Responses;
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

        public Task<Result> CreateCustomerAsync(UpsertCustomerRequest request)
        {
            return _mediator.Send(request);
        }

        public Task<Result> DeleteCustomerAsync(DeleteCustomerRequest request)
        {
            return _mediator.Send(request);
        }

        public Task<Result<GetCustomerResponse>> GetCustomerAsync(GetCustomerByIdRequest request)
        {
            return _mediator.Send(request);
        }

        public Task<Result<GetCustomerResponse>> GetCustomerAsync(GetCustomerByUserNameRequest request)
        {
            return _mediator.Send(request);
        }
    }
}