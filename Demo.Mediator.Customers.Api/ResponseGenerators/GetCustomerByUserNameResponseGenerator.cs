using System.Net;
using AutoMapper;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.Models.Requests;
using Demo.Mediator.Customers.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Mediator.Customers.Api.ResponseGenerators
{
    public class GetCustomerByUserNameResponseGenerator : IResponseGenerator<GetCustomerByUserNameRequest, GetCustomerResponse>
    {
        private readonly IMapper _mapper;

        public GetCustomerByUserNameResponseGenerator(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult GenerateResponse(GetCustomerByUserNameRequest request, Result<GetCustomerResponse> operation)
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