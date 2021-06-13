using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Core;
using MediatR;

namespace Demo.Mediator.Customers.Api.DataAccess.Commands
{
    public class DeleteCustomerCommand : CommandBase
    {
        public string CustomerId { get; set; }
    }
    
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result>
    {
        public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return Result.Success();
        }
    }
}