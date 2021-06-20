using System.Threading;
using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.DataAccess.Models;
using Demo.Mediator.Customers.Api.DataAccess.Queries;
using Demo.Mediator.Customers.IntegrationTests.MockData;
using MediatR;

namespace Demo.Mediator.Customers.IntegrationTests.DataAccess
{
    public class TestGetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<CustomerDataModel>>
    {
        public async Task<Result<CustomerDataModel>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var operation = await AssemblyResourceFileReader.GetFileContentAsync<Result<CustomerDataModel>>(request.Id);
            return operation;
        }
    }
}