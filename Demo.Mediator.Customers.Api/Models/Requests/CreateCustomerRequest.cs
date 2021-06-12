using Demo.Mediator.Customers.Api.Core;
using MediatR;

namespace Demo.Mediator.Customers.Api.Models.Requests
{
    public class CreateCustomerRequest : IRequest<Result>
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}