using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Demo.Mediator.Customers.Api.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Mediator.Customers.Api.Extensions
{   
    public static class DependencyInjectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services, params Assembly[] assemblies)
        {
            var assemblyList = assemblies?.ToList() ?? new List<Assembly>();
            if (!assemblyList.Any())
            {
                return;
            }

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().ToList();

                var candidateTypes = types.Where(x => !x.IsAbstract && x.GetCustomAttribute<InjectMeAttribute>(true) != null).ToList();

                RegisterCandidates(services, candidateTypes);
            }

        }

        private static void RegisterCandidates(IServiceCollection services, List<Type> candidateTypes)
        {
            foreach (var candidateType in candidateTypes)
            {
                var attribute = candidateType.GetCustomAttribute<InjectMeAttribute>(true);
                var lifeTime = attribute.Lifetime;
                var injectAs = attribute.InjectAs;

                if (injectAs == InjectAs.AsImplementedInterfaces)
                {
                    RegisterAsInterfaces(services, lifeTime, candidateType);
                }
                else if (injectAs == InjectAs.AsSelf)
                {
                    services.Add(new ServiceDescriptor(candidateType, candidateType, lifeTime));
                }
            }
        }

        private static void RegisterAsInterfaces(IServiceCollection services, ServiceLifetime lifeTime, Type candidateType)
        {
            var interfaces = candidateType.GetInterfaces()?.ToList();
            if (interfaces != null && interfaces.Any())
            {
                foreach (var @interface in interfaces)
                {
                    if (@interface.IsGenericType)
                    {
                        var sourceType = @interface.GetGenericTypeDefinition().MakeGenericType(@interface.GetGenericArguments());
                        services.Add(new ServiceDescriptor(sourceType, candidateType, lifeTime));
                    }
                    else
                    {
                        services.Add(new ServiceDescriptor(@interface, candidateType, lifeTime));
                    }
                }
            }
        }
    }
}