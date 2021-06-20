using System.Collections.Generic;
using System.Reflection;
using Demo.Mediator.Customers.Api;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.DataAccess.Models;
using Demo.Mediator.Customers.Api.DataAccess.Queries;
using Demo.Mediator.Customers.Api.Extensions;
using Demo.Mediator.Customers.Api.Models.Requests;
using Demo.Mediator.Customers.Api.Models.Responses;
using Demo.Mediator.Customers.IntegrationTests.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Server;

namespace Demo.Mediator.Customers.IntegrationTests
{
    public class IntegrationTestFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup:class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            var mockServer = WireMockServer.Start();
            
            // builder.ConfigureAppConfiguration(configurationBuilder =>
            //     {
            //         configurationBuilder.AddInMemoryCollection(new[]
            //         {
            //             new KeyValuePair<string, string>("CustomerIdentity:BaseUrl", mockServer.Urls[0])
            //         });
            //     })
            //     .ConfigureServices(collection =>
            //     {
            //         collection.AddSingleton(mockServer);
            //     })

            builder.ConfigureServices(services =>
            {
                services.AddSingleton(mockServer);
                services.AddScoped<IRequestHandler<GetCustomerByIdQuery, Result<CustomerDataModel>>, TestGetCustomerByIdQueryHandler>();
            });
        }
    }
}