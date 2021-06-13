using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace Demo.Mediator.Customers.Api.DataAccess
{
    public interface ITableStorageFactory
    {
        Task<CloudTable> GetTableAsync(string tableName);
    }
}