using System.Threading;
using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.DataAccess.Models;
using Demo.Mediator.Customers.Api.DataAccess.Queries;
using Demo.Mediator.Customers.IntegrationTests.MockData;
using MediatR;

namespace Demo.Mediator.Customers.IntegrationTests.DataAccess
{
    public class TestGetCustomerByUserNameQueryHandler : IRequestHandler<GetCustomerByUserNameQuery, Result<CustomerDataModel>>
    {
        public async Task<Result<CustomerDataModel>> Handle(GetCustomerByUserNameQuery request, CancellationToken cancellationToken)
        {
            var customerData = await AssemblyResourceFileReader.GetFileContentAsync<CustomerDataModel>(request.UserName);
            return Result<CustomerDataModel>.Success(customerData);
        }
    }
}