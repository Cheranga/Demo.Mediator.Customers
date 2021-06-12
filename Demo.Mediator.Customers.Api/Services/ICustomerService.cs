using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.Models.Assets;
using Demo.Mediator.Customers.Api.Models.Requests;

namespace Demo.Mediator.Customers.Api.Services
{
    public interface ICustomerService
    {
        Task<Result> CreateCustomerAsync(CreateCustomerRequest request);
        Task<Result<Customer>> GetCustomerAsync(GetCustomerByIdRequest request);
    }
}