using Demo.Mediator.Customers.Api.Core;
using MediatR;

namespace Demo.Mediator.Customers.Api.DataAccess.Commands
{
    public abstract class CommandBase : IRequest<Result>
    {
    }
}