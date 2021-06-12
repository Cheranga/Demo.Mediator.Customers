using System.Net;
using AutoMapper;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.Models.Requests;
using Demo.Mediator.Customers.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Mediator.Customers.Api.ResponseGenerators
{
    public class GetCustomerByIdResponseGenerator : IResponseGenerator<GetCustomerByIdRequest, GetCustomerResponse>
    {
        private readonly IMapper _mapper;

        public GetCustomerByIdResponseGenerator(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult GenerateResponse(GetCustomerByIdRequest request, Result<GetCustomerResponse> operation)
        {
            if (operation.Status)
            {
                return new OkObjectResult(operation.Data);
            }

            if (operation.Data == null)
            {
                return new NotFoundResult();
            }

            var errorResponse = _mapper.Map<ErrorResponse>(operation.ValidationResult);
            return new ObjectResult(errorResponse)
            {
                StatusCode = (int) HttpStatusCode.UnprocessableEntity
            };
        }
    }
}