using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.Models.Assets;
using MediatR;

namespace Demo.Mediator.Customers.Api.DataAccess.Queries
{
    public class GetCustomerByIdQuery : IRequest<Result<Customer>>
    {
        public string Id { get; set; }
    }

    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<Customer>>
    {
        public Task<Result<Customer>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}