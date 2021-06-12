using System;
using System.Linq;
using System.Reflection.Emit;
using Demo.Mediator.Customers.Api.Core;
using Demo.Mediator.Customers.Api.DataAccess.Commands;
using Demo.Mediator.Customers.Api.DataAccess.Queries;
using Demo.Mediator.Customers.Api.Mappers;
using Demo.Mediator.Customers.Api.Models.Assets;
using Demo.Mediator.Customers.Api.Models.Requests;
using Demo.Mediator.Customers.Api.Models.Responses;
using Demo.Mediator.Customers.Api.ResponseGenerators;
using Demo.Mediator.Customers.Api.Services;
using Demo.Mediator.Customers.Api.Validators;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Demo.Mediator.Customers.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Demo.Mediator.Customers.Api", Version = "v1"}); });

            RegisterValidators(services);
            RegisterMappers(services);
            RegisterServices(services);
            RegisterResponseGenerators(services);
            RegisterMediators(services);
        }

        private void RegisterResponseGenerators(IServiceCollection services)
        {
            services.AddScoped<IResponseGenerator<GetCustomerByIdRequest, GetCustomerResponse>, GetCustomerByIdResponseGenerator>();
            services.AddScoped<IResponseGenerator<UpsertCustomerRequest>, UpsertCustomerResponseGenerator>();
            
            services.AddScoped<IResponseGeneratorFactory, ResponseGeneratorFactory>(provider =>
            {
                return new ResponseGeneratorFactory(provider, provider.GetRequiredService<ILogger<ResponseGeneratorFactory>>());
            });
        }

        private void RegisterMediators(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(Startup).Assembly
            };

            services.AddMediatR(assemblies, configuration =>
            {
                configuration.AsScoped();
            });

            Test(services);
            // services.AddScoped(typeof(IPipelineBehavior<UpsertCustomerRequest,Result>), typeof(CommandPerformanceBehaviour<UpsertCustomerRequest>));
            // services.AddScoped(typeof(IPipelineBehavior<CreateCustomerCommand,Result>), typeof(CommandPerformanceBehaviour<CreateCustomerCommand>));
            // services.AddScoped(typeof(IPipelineBehavior<UpdateCustomerCommand,Result>), typeof(CommandPerformanceBehaviour<UpdateCustomerCommand>));
            services.AddScoped(typeof(IPipelineBehavior<GetCustomerByIdRequest, Result<GetCustomerResponse>>), typeof(QueryPerformanceBehaviour<GetCustomerByIdRequest, GetCustomerResponse>));
            services.AddScoped(typeof(IPipelineBehavior<GetCustomerByIdQuery, Result<Customer>>), typeof(QueryPerformanceBehaviour<GetCustomerByIdQuery, Customer>));
            
            // services.AddScoped(typeof(IPipelineBehavior<UpsertCustomerRequest, Result>), typeof(CommandValidationBehaviour<UpsertCustomerRequest>));
            // services.AddScoped(typeof(IPipelineBehavior<UpdateCustomerCommand, Result>), typeof(CommandValidationBehaviour<UpdateCustomerCommand>));
            
            
            services.AddScoped(typeof(IPipelineBehavior<GetCustomerByIdRequest, Result<GetCustomerResponse>>), typeof(QueryValidationBehaviour<GetCustomerByIdRequest, GetCustomerResponse>));
            services.AddScoped(typeof(IPipelineBehavior<GetCustomerByIdQuery, Result<Customer>>), typeof(QueryValidationBehaviour<GetCustomerByIdQuery, Customer>));
        }

        private void Test(IServiceCollection services)
        {
            var commandTypes = typeof(Startup).Assembly.GetTypes()
                .Where(x => x.IsClass && x.BaseType == typeof(CommandBase)).ToList();

            foreach (var commandType in commandTypes)
            {
                var sourceType = typeof(IPipelineBehavior<,>).MakeGenericType(commandType, typeof(Result));
                var performanceImplementationType = typeof(CommandPerformanceBehaviour<>).MakeGenericType(commandType);
                var validationImplementationType = typeof(CommandValidationBehaviour<>).MakeGenericType(commandType);

                services.AddScoped(sourceType, performanceImplementationType);
                services.AddScoped(sourceType, validationImplementationType);
            }
        }

        private void RegisterValidators(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(ModelValidatorBase<>).Assembly
            };

            services.AddValidatorsFromAssemblies(assemblies);
        }

        private void RegisterMappers(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(MappingProfile).Assembly
            };

            services.AddAutoMapper(assemblies,ServiceLifetime.Scoped);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo.Mediator.Customers.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}