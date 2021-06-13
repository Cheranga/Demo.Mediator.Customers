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
    public class GetCustomerByIdQuery : QueryBase<CustomerDataModel>
    {
        public string Id { get; set; }
    }

    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<CustomerDataModel>>
    {
        private readonly ITableStorageFactory _tableStorageFactory;

        public GetCustomerByIdQueryHandler(ITableStorageFactory tableStorageFactory)
        {
            _tableStorageFactory = tableStorageFactory;
        }
        
        public async Task<Result<CustomerDataModel>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customersTable = await _tableStorageFactory.GetTableAsync("Customers");
            
            var partitionQuery = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "active".ToUpper());
            var rowIdQuery = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, request.Id.ToUpper());
            var combinedQuery = TableQuery.CombineFilters(partitionQuery, TableOperators.And, rowIdQuery);
            
            var getRecordQuery = new TableQuery<CustomerDataModel>().Where(combinedQuery);
            var queryOperation = await customersTable.ExecuteQuerySegmentedAsync(getRecordQuery, new TableContinuationToken(), cancellationToken);
            
            var customer = queryOperation?.Results?.FirstOrDefault();

            return Result<CustomerDataModel>.Success(customer);
        }
    }
}