using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Core;
using MediatR;

namespace Demo.Mediator.Customers.Api.DataAccess.Commands
{
    public class CreateCustomerCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result>
    {
        public Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}