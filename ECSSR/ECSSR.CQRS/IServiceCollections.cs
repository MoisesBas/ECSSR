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
        public static IServiceCollection AddEntitCommand<TDbContext, TKey, TEntity, TCreateModel,TUpdateModel, TReadModel>(this IServiceCollection services)
        where TDbContext : IECSSRDbContext
        where TEntity : class, IHaveIdentifier<TKey>, new()
        {
            services.TryAddTransient<IRequestHandler<EntityUpdateCommand<TKey, TUpdateModel, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, EntityUpdateCommandHandler<TDbContext, TEntity, TKey, TUpdateModel, TReadModel>>();
            services.TryAddTransient<IRequestHandler<EntityDeleteCommand<TKey, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, EntityDeleteCommandHandler<TDbContext, TEntity, TKey, TReadModel>>();
            services.TryAddTransient<IRequestHandler<EntityCreateCommand<TCreateModel, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, EntityCreateCommandHandler<TDbContext, TEntity, TKey, TCreateModel, TReadModel>>();
            return services;
        }
    }
}
