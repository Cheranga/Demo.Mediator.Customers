using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Models.Requests;
using Demo.Mediator.Customers.Api.ResponseGenerators;
using Demo.Mediator.Customers.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo.Mediator.Customers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IResponseGeneratorFactory _responseGenerator;

        public CustomersController(ICustomerService customerService, IResponseGeneratorFactory responseGenerator)
        {
            _customerService = customerService;
            _responseGenerator = responseGenerator;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomer([FromRoute] GetCustomerByIdRequest request)
        {
            var operation = await _customerService.GetCustomerAsync(request);
            var response = _responseGenerator.GenerateResponse(request, operation);

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            var operation = await _customerService.CreateCustomerAsync(request);
            var response = _responseGenerator.GenerateResponse(request, operation);

            return response;
        }
    }
}