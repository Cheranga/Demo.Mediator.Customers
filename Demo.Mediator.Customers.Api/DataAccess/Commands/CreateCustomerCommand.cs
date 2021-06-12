using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Core;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Mediator.Customers.Api.DataAccess.Commands
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LogPerformanceAttribute : Attribute
    {
        private ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Scoped;
    }
    
    [LogPerformance]
    public abstract class CommandBase : IRequest<Result>
    {
        
    }

    public abstract class QueryBase<TResponse> : IRequest<Result<TResponse>>
    {
        
    }
    
    public class CreateCustomerCommand : CommandBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result>
    {
        public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return Result.Success();
        }
    }
}