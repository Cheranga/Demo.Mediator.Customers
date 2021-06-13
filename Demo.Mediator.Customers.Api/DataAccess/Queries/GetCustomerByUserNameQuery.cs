using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.Models.Assets;
using MediatR;

namespace Demo.Mediator.Customers.Api.DataAccess.Queries
{
    public class GetCustomerByUserNameQuery : QueryBase<Customer>
    {
        public string UserName { get; set; }
    }
    
    public class GetCustomerByUserNameQueryHandler : IRequestHandler<GetCustomerByUserNameQuery, Result<Customer>>
    {
        public async Task<Result<Customer>> Handle(GetCustomerByUserNameQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return Result<Customer>.Success(new Customer
            {
                Id = Guid.NewGuid().ToString("N"),
                Address = "Melbourne",
                Name = "Cheranga Hatangala",
                UserName = "cheranga@gmail.com"
            });
        }
    }
}