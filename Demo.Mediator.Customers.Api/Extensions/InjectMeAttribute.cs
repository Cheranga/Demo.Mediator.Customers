using System;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Mediator.Customers.Api.Extensions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectMeAttribute : Attribute
    {
        public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Scoped;
        public InjectAs InjectAs { get; set; } = InjectAs.AsImplementedInterfaces;
    }

    public enum InjectAs
    {
        AsSelf,
        AsImplementedInterfaces
    }
}