using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Demo.Mediator.Customers.Api.DataAccess.Models;
using Demo.Mediator.Customers.Api.Models.Assets;
using Demo.Mediator.Customers.Api.Models.Responses;

namespace Demo.Mediator.Customers.Api.Mappers
{
    public class CustomerToGetCustomerResponseMapper : ITypeConverter<CustomerDataModel, GetCustomerResponse>
    {
        public GetCustomerResponse Convert(CustomerDataModel source, GetCustomerResponse destination, ResolutionContext context)
        {
            if (source == null)
            {
                return destination;
            }

            destination ??= new GetCustomerResponse();

            var nameParty = source.Name?.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)?.ToList();
            var firstName = nameParty?.FirstOrDefault();
            var lastName = string.Join(" ", nameParty?.Skip(1) ?? new List<string>());

            destination.FirstName = firstName;
            destination.LastName = lastName;
            destination.Address = source.Address;

            return destination;
        }
    }
}