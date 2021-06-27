using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Demo.Mediator.Customers.Api;
using Demo.Mediator.Customers.Api.Constants;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.DataAccess.Models;
using Demo.Mediator.Customers.Api.DataAccess.Queries;
using Demo.Mediator.Customers.Api.Models.Responses;
using Demo.Mediator.Customers.IntegrationTests.DataAccess;
using Demo.Mediator.Customers.IntegrationTests.Util;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TestStack.BDDfy;
using Xunit;

namespace Demo.Mediator.Customers.IntegrationTests.Endpoints
{
    public class CustomersControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _applicationFactory;
        private HttpClient _client;
        private HttpResponseMessage _httpResponse;
        private string _userName;

        public CustomersControllerTests(WebApplicationFactory<Startup> applicationFactory)
        {
            _applicationFactory = applicationFactory;
            _applicationFactory.ClientOptions.BaseAddress = new Uri("https://localhost:5001/customers/");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public Task GetCustomerByUserName_WillReturnErrorResponse_WhenUserNameIsEmptyOrNull(string userName)
        {
            this.Given(x => GivenUserNameIsLikeEmpty(userName))
                .When(x => WhenGetCustomerByUserNameEndpointIsCalled())
                .Then(x => ThenMustReturnStatusCode(HttpStatusCode.NotFound))
                .BDDfy();

            return Task.CompletedTask;
        }

        [Theory]
        [InlineData("blah")]
        public Task GetCustomerByUserName_WillReturnErrorResponse_WhenCustomerDoesNotExist(string userName)
        {
            this.Given(x => GivenUserNameDoesNotExist(userName))
                .When(x => WhenGetCustomerByUserNameEndpointIsCalled())
                .Then(x => ThenMustReturnStatusCode(HttpStatusCode.NotFound))
                .And(x => ThenMustReturnErrorResponseForGetCustomerByUserName(ErrorCodes.CustomerNotFound))
                .BDDfy();

            return Task.CompletedTask;
        }

        [Fact]
        public Task GetCustomerByUserName_WillReturnCustomerDetails_WhenCustomerExists()
        {
            this.Given(x => GivenCustomerExistsForProvidedUserName())
                .When(x => WhenGetCustomerByUserNameEndpointIsCalled())
                .Then(x => ThenMustReturnCustomerDetails())
                .BDDfy();

            return Task.CompletedTask;
        }

        private async Task ThenMustReturnCustomerDetails()
        {
            var customerData = await HttpResponseReader.Get<GetCustomerResponse>(_httpResponse);
        }

        private Task GivenCustomerExistsForProvidedUserName()
        {
            _userName = "gcu_cheranga";
            _client = _applicationFactory.WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestServices(
                        services =>
                        {
                            services.AddScoped<IRequestHandler<GetCustomerByUserNameQuery, Result<CustomerDataModel>>>(_ => new TestGetCustomerByUserNameQueryHandler());
                        });
                }).CreateClient();

            return Task.CompletedTask;
        }


        private Task GivenUserNameIsLikeEmpty(string userName)
        {
            _userName = userName;
            _client = _applicationFactory.CreateClient();

            var a = 1;
            if (a > 0)
            {
                Console.WriteLine("");
            }

            return Task.CompletedTask;
        }

        private async Task ThenMustReturnErrorResponseForGetCustomerByUserName(string errorCode)
        {
            var model = await HttpResponseReader.Get<ErrorResponse>(_httpResponse);

            model.Should().NotBeNull();
            model.ErrorCode.Should().Be(errorCode);
        }

        private Task ThenMustReturnStatusCode(HttpStatusCode expectedStatusCode)
        {
            _httpResponse.StatusCode.Should().Be(expectedStatusCode);
            return Task.CompletedTask;
        }

        private async Task WhenGetCustomerByUserNameEndpointIsCalled()
        {
            _httpResponse = await _client.GetAsync($"search/username/{_userName}");
        }

        private void GivenUserNameDoesNotExist(string userName)
        {
            _userName = userName;
            _client = _applicationFactory.CreateClient();
        }
    }
}