using AutoMapper;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.DataAccess.Commands;
using Demo.Mediator.Customers.Api.DataAccess.Models;
using Demo.Mediator.Customers.Api.DataAccess.Queries;
using Demo.Mediator.Customers.Api.Models.Assets;
using Demo.Mediator.Customers.Api.Models.Requests;
using Demo.Mediator.Customers.Api.Models.Responses;
using FluentValidation.Results;
using Microsoft.Azure.Cosmos.Table;

namespace Demo.Mediator.Customers.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateCustomerCommand, CustomerDataModel>().ConvertUsing<UpdateCustomerDataModelMapper>();
            CreateMap<CreateCustomerCommand, CustomerDataModel>().ConvertUsing<CustomerDataModelMapper>();
            CreateMap<GetCustomerByIdRequest, GetCustomerByIdQuery>().ForMember(x => x.Id, x => x.MapFrom(y => y.CustomerId));
            CreateMap<GetCustomerByUserNameRequest, GetCustomerByUserNameQuery>().ForMember(x => x.UserName, x => x.MapFrom(y => y.UserName));
            CreateMap<DeleteCustomerRequest, GetCustomerByIdRequest>();
            CreateMap<DeleteCustomerRequest, DeleteCustomerCommand>();
            CreateMap<CustomerDataModel, GetCustomerResponse>().ConvertUsing<CustomerToGetCustomerResponseMapper>();
            CreateMap<UpsertCustomerRequest, UpdateCustomerCommand>();
            CreateMap<UpsertCustomerRequest, CreateCustomerCommand>();
            CreateMap<UpsertCustomerRequest, GetCustomerByIdRequest>().ForMember(x => x.CustomerId, x => x.MapFrom(y => y.Id));
            CreateMap(typeof(Result<>), typeof(ErrorResponse)).ConvertUsing(typeof(ErrorResponseMapper<>));
        }
    }
}