Demo.Mediator.Customers


### Integration Tests

> Prerequisites

* Install `Microsoft.AspNetCore.Mvc.Testing` nuget package.
* Change the project properties from `Microsoft.NET.Sdk` to `Microsoft.NET.Sdk.Web` so that it will have the same build experience as the web project.
* Disable shadow copying so that the tests will be executed in the same build output as the web project.
  * Add an `xunit.runner.json` file and update its contents as shown below.
    
```json
{
  "shadowCopy": false  
}
```
---
> Overriding dependencies

* Using `WithWebHostBuilder` in `WebApplicationFactory` and using the method `ConfigureTestServices` override the services when testing.

```c#
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
```

