using System;
using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECSSR.COMMON
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainAutoMapper(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
