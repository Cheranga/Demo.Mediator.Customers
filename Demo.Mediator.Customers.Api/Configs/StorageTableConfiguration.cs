using Microsoft.Azure.Cosmos.Table;

namespace Demo.Mediator.Customers.Api.Configs
{
    public class StorageTableConfiguration
    {
        public string ConnectionString { get; set; }
        public string CustomersTable { get; set; }
    }
}