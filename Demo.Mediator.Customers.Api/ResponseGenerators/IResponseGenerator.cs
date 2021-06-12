using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Core;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Mediator.Customers.Api.ResponseGenerators
{
    public interface IResponseGenerator<TRequest, TResponse>
    {
        Task<IActionResult> GenerateResponse(TRequest request, Result<TResponse> operation);
    }

    public interface IResponseGenerator<TRequest>
    {
        Task<IActionResult> GenerateResponse(TRequest request, Result operation);
    }
}