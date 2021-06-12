using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.Models.Assets;
using MediatR;

namespace Demo.Mediator.Customers.Api.Models.Requests
{
    public class GetCustomerByIdRequest : IRequest<Result<Customer>>
    {
        public string CustomerId { get; set; }
    }
}