using System.Net;
using AutoMapper;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.Models.Requests;
using Demo.Mediator.Customers.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Mediator.Customers.Api.ResponseGenerators
{
    public class UpsertCustomerResponseGenerator: IResponseGenerator<UpsertCustomerRequest>
    {
        private readonly IMapper _mapper;

        public UpsertCustomerResponseGenerator(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        public IActionResult GenerateResponse(UpsertCustomerRequest request, Result operation)
        {
            if (operation.Status)
            {
                return new OkResult();
            }

            
            var errorResponse = _mapper.Map<ErrorResponse>(operation.ValidationResult);
            return new ObjectResult(errorResponse)
            {
                StatusCode = (int) HttpStatusCode.UnprocessableEntity
            };
        }
    }
}