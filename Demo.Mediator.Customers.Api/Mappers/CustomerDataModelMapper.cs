using System;
using AutoMapper;
using Demo.Mediator.Customers.Api.DataAccess.Commands;
using Demo.Mediator.Customers.Api.DataAccess.Models;
using Demo.Mediator.Customers.Api.Extensions;

namespace Demo.Mediator.Customers.Api.Mappers
{
    public class CustomerDataModelMapper : ITypeConverter<CreateCustomerCommand, CustomerDataModel>
    {
        public CustomerDataModel Convert(CreateCustomerCommand source, CustomerDataModel destination, ResolutionContext context)
        {
            var partitionKey = "active".ToUpper();
            var customerId = Guid.NewGuid().ToString("N").ToUpper();

            var dataModel = new CustomerDataModel
            {
                PartitionKey = partitionKey,
                RowKey = customerId,
                Id = customerId,
                Address = source.Address,
                Name = source.Name,
                UserName = source.UserName
            };

            return dataModel;
        }
    }
    
    public class UpdateCustomerDataModelMapper : ITypeConverter<UpdateCustomerCommand, CustomerDataModel>
    {
        public CustomerDataModel Convert(UpdateCustomerCommand source, CustomerDataModel destination, ResolutionContext context)
        {
            var partitionKey = "active".ToUpper();

            var dataModel = new CustomerDataModel
            {
                PartitionKey = partitionKey,
                RowKey = source.Id.ToUpper(),
                Id = source.Id,
                Address = source.Address.IsEmpty()? null : source.Address,
                Name = source.Name.IsEmpty()? null : source.Name,
                UserName = source.UserName.IsEmpty()? null: source.UserName
            };

            return dataModel;
        }
    }
}