using System;
using ECSSR.COMMON.Commands;
using ECSSR.CQRS.Handlers;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ECSSR.CQRS
{
    public static class IServiceCollections
    {
        public static IServiceCollection AddEntitCommand<TUnitOfWork, TKey, TEntity, TCreateModel, TReadModel>(this IServiceCollection services)
        where TUnitOfWork : IECSSRDbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
        {
            services.TryAddScoped<IRequestHandler<EntityCreateCommand<TCreateModel, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, EntityCreateCommandHandler<TUnitOfWork, TEntity, TKey, TCreateModel, TReadModel>>();
            return services;
        }
    }
}
