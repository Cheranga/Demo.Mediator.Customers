using Demo.Mediator.Customers.Api.Core;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Mediator.Customers.Api.ResponseGenerators
{
    public interface IResponseGenerator<in TRequest, TResponse>
    {
        IActionResult GenerateResponse(TRequest request, Result<TResponse> operation);
    }

    public interface IResponseGenerator<in TRequest>
    {
        IActionResult GenerateResponse(TRequest request, Result operation);
    }
}