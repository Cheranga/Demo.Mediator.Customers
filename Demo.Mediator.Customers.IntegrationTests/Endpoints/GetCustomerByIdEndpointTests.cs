using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Demo.Mediator.Customers.Api;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.DataAccess.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TestStack.BDDfy;
using WireMock.Server;
using Xunit;

namespace Demo.Mediator.Customers.IntegrationTests.Endpoints
{
    public class GetCustomerByIdEndpointTests : IClassFixture<IntegrationTestFactory<Startup>>
    {
        private readonly IntegrationTestFactory<Startup> _factory;
        private readonly WireMockServer _mockServer;
        private string _customerId;
        private HttpResponseMessage _httpResponse;

        public GetCustomerByIdEndpointTests(IntegrationTestFactory<Startup> factory)
        {
            _factory = factory;
            _mockServer = _factory.Services.GetRequiredService<WireMockServer>();
        }

        [Fact]
        public Task CannotGetCustomerDataSuccessfullyFromDataStore()
        {   
            this.Given(x => GivenCustomerCanBeRetrievedSuccessfullyFromDataStore())
                .When(x => WhenGetCustomerByIdEndpointIsCalled())
                .Then(x => ThenWillReturnSuccessfulResponse())
                .BDDfy();

            return Task.CompletedTask;
        }

        private Task GivenCustomerCanBeRetrievedSuccessfullyFromDataStore()
        {
            _customerId = "gci_666";
            return Task.CompletedTask;
        }

        [Fact]
        public Task GettingCustomerDataSuccessfullyFromDataStore()
        {
            this.Given(x => GivenCustomerDataCanBeRetrievedSuccessfully())
                .When(x => WhenGetCustomerByIdEndpointIsCalled())
                .Then(x => ThenWillReturnSuccessfulResponse())
                .BDDfy();

            return Task.CompletedTask;
        }

        private Task ThenWillReturnSuccessfulResponse()
        {
            _httpResponse.IsSuccessStatusCode.Should().BeTrue();
            _httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            return Task.CompletedTask;
        }

        private Task GivenCustomerDataCanBeRetrievedSuccessfully()
        {
            _customerId = "gci_666";
            return Task.CompletedTask;
        }

        private async Task ThenWillReturnErrorResponse()
        {
            _httpResponse.IsSuccessStatusCode.Should().BeFalse();
            _httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private async Task WhenGetCustomerByIdEndpointIsCalled()
        {
            var client = _factory.CreateClient();
            _httpResponse = await client.GetAsync($"customers/search/id/{_customerId}");
        }

        private Task GivenThereIsAnErrorWhenAccessingDataStore()
        {
            _customerId = "error_gci_666";
            return Task.CompletedTask;
        }
    }
}