using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.DataAccess.Models;
using Demo.Mediator.Customers.Api.Models.Assets;
using MediatR;
using Microsoft.Azure.Cosmos.Table;

namespace Demo.Mediator.Customers.Api.DataAccess.Queries
{
    public class GetCustomerByUserNameQuery : QueryBase<CustomerDataModel>
    {
        public string UserName { get; set; }
    }
    
    public class GetCustomerByUserNameQueryHandler : IRequestHandler<GetCustomerByUserNameQuery, Result<CustomerDataModel>>
    {
        private readonly ITableStorageFactory _tableStorageFactory;

        public GetCustomerByUserNameQueryHandler(ITableStorageFactory tableStorageFactory)
        {
            _tableStorageFactory = tableStorageFactory;
        }
        
        public async Task<Result<CustomerDataModel>> Handle(GetCustomerByUserNameQuery request, CancellationToken cancellationToken)
        {
            var customersTable = await _tableStorageFactory.GetTableAsync("Customers");
            
            var partitionQuery = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "active".ToUpper());
            var rowIdQuery = TableQuery.GenerateFilterCondition("UserName", QueryComparisons.Equal, request.UserName);
            var combinedQuery = TableQuery.CombineFilters(partitionQuery, TableOperators.And, rowIdQuery);
            
            var getRecordQuery = new TableQuery<CustomerDataModel>().Where(combinedQuery);
            var queryOperation = await customersTable.ExecuteQuerySegmentedAsync(getRecordQuery, new TableContinuationToken(), cancellationToken);
            
            var customer = queryOperation?.Results?.FirstOrDefault();

            return Result<CustomerDataModel>.Success(customer);
        }
    }
}