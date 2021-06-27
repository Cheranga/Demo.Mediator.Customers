using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.DataAccess.Models;
using Demo.Mediator.Customers.Api.DataAccess.Queries;
using Demo.Mediator.Customers.IntegrationTests.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Mediator.Customers.IntegrationTests
{
    public class IntegrationTestFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // var mockServer = WireMockServer.Start();

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

            // builder.ConfigureServices(services =>
            // {
            //     services.AddSingleton(mockServer);
            //     services.AddScoped<IRequestHandler<GetCustomerByIdQuery, Result<CustomerDataModel>>, TestGetCustomerByIdQueryHandler>();
            // });
        }
    }
}