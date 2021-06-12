using System;
using Demo.Mediator.Customers.Api.Core;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Mediator.Customers.Api.ResponseGenerators
{
    public interface IResponseGeneratorFactory
    {
        IActionResult GenerateResponse<TRequest, TResponse>(TRequest request, Result<TResponse> operation);
        IActionResult GenerateResponse<TRequest>(TRequest request, Result operation);
    }

    public class ResponseGeneratorFactory : IResponseGeneratorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ResponseGeneratorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IActionResult GenerateResponse<TRequest, TResponse>(TRequest request, Result<TResponse> operation)
        {
            throw new NotImplementedException();
        }

        public IActionResult GenerateResponse<TRequest>(TRequest request, Result operation)
        {
            throw new NotImplementedException();
        }
    }
}