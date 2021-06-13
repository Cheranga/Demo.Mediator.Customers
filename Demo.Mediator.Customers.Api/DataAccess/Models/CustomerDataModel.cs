using Microsoft.Azure.Cosmos.Table;

namespace Demo.Mediator.Customers.Api.DataAccess.Models
{
    public class CustomerDataModel : TableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
    }
}