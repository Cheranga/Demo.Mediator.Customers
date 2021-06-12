using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Core;
using MediatR;

namespace Demo.Mediator.Customers.Api.DataAccess.Commands
{
    public class UpdateCustomerCommand : CommandBase//IRequest<Result>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
    
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result>
    {
        public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return Result.Success();
        }
    }
}