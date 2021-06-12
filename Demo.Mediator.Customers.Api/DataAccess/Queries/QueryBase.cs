using Demo.Mediator.Customers.Api.Core;
using MediatR;

namespace Demo.Mediator.Customers.Api.DataAccess.Queries
{
    public abstract class QueryBase<TResponse> : IRequest<Result<TResponse>>
    {
    }
}