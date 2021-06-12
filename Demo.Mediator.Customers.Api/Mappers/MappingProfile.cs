using System.ComponentModel;
using AutoMapper;
using Demo.Mediator.Customers.Api.DataAccess.Commands;
using Demo.Mediator.Customers.Api.DataAccess.Queries;
using Demo.Mediator.Customers.Api.Models.Assets;
using Demo.Mediator.Customers.Api.Models.Requests;
using Demo.Mediator.Customers.Api.Models.Responses;
using FluentValidation.Results;

namespace Demo.Mediator.Customers.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetCustomerByIdRequest, GetCustomerByIdQuery>().ForMember(x=>x.Id, x=> x.MapFrom(y=> y.CustomerId ));
            CreateMap<Customer, GetCustomerResponse>().ConvertUsing<CustomerToGetCustomerResponseMapper>();
            CreateMap<UpsertCustomerRequest, UpdateCustomerCommand>();
            CreateMap<UpsertCustomerRequest, CreateCustomerCommand>();
            CreateMap<UpsertCustomerRequest, GetCustomerByIdRequest>().ForMember(x => x.CustomerId, x => x.MapFrom(y => y.Id));
            CreateMap<ValidationResult, ErrorResponse>().ConvertUsing<ErrorResponseMapper>();
        }
    }
}