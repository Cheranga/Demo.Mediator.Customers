using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.Models.Requests;
using Demo.Mediator.Customers.Api.Models.Responses;

namespace Demo.Mediator.Customers.Api.Services
{
    public interface ICustomerService
    {
        Task<Result> CreateCustomerAsync(UpsertCustomerRequest request);
        Task<Result<GetCustomerResponse>> GetCustomerAsync(GetCustomerByIdRequest request);
    }
}