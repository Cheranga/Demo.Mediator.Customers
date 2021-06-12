using System;
using System.Collections.Generic;
using System.Net;
using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<ResponseGeneratorFactory> _logger;

        public ResponseGeneratorFactory(IServiceProvider serviceProvider, ILogger<ResponseGeneratorFactory> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public IActionResult GenerateResponse<TRequest, TResponse>(TRequest request, Result<TResponse> operation)
        {
            var responseGenerator = _serviceProvider.GetRequiredService<IResponseGenerator<TRequest, TResponse>>();
            if (responseGenerator == null)
            {
                return GetErrorResponse<TRequest>();
            }

            var response = responseGenerator.GenerateResponse(request, operation);
            return response;
        }

        public IActionResult GenerateResponse<TRequest>(TRequest request, Result operation)
        {
            var responseGenerator = _serviceProvider.GetRequiredService<IResponseGenerator<TRequest>>();
            if (responseGenerator == null)
            {
                return GetErrorResponse<TRequest>();
            }

            var response = responseGenerator.GenerateResponse(request, operation);
            return response;
        }

        private IActionResult GetErrorResponse<TRequest>()
        {
            _logger.LogError("No response generator for {RequestType}", typeof(TRequest).Name);
            var errorResponse = new ErrorResponse
            {
                ErrorCode = ErrorCodes.InternalServerError,
                ErrorMessage = ErrorCodes.InternalServerError
            };

            return new ObjectResult(errorResponse)
            {
                StatusCode = (int) (HttpStatusCode.InternalServerError)
            };
        }
    }
}