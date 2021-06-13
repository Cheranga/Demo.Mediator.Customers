using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Configs;
using Demo.Mediator.Customers.Api.Extensions;
using Microsoft.Azure.Cosmos.Table;

namespace Demo.Mediator.Customers.Api.DataAccess
{
    public class StorageTableFactory : ITableStorageFactory
    {
        private readonly CloudTableClient _tableClient;

        public StorageTableFactory(StorageTableConfiguration configuration)
        {
            var storageAccount = CloudStorageAccount.Parse(configuration.ConnectionString);
            _tableClient = storageAccount.CreateCloudTableClient();
        }

        public async Task<CloudTable> GetTableAsync(string tableName)
        {
            if (tableName.IsEmpty())
            {
                return null;
            }

            var table = _tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();
            return table;
        }
    }
}