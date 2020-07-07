using System;
using System.Reflection;
using AutoMapper;
using ECSSR.COMMON.Commands;
using ECSSR.COMMON.Product.Dto;
using ECSSR.COMMON.Handlers;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ECSSR.COMMON
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainCommon(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddEntitCommand<IECSSRDbContext, int, DOMAIN.Entities.Product, ProductCreateDto, ProductUpdateDto, ProductReadDto>();
            return serviceCollection;
        }
            public static IServiceCollection AddDomainAutoMapper(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
            return serviceCollection;
        }

        private static IServiceCollection AddEntitCommand<TDbContext, TKey, TEntity, TCreateModel, TUpdateModel, TReadModel>(this IServiceCollection services)
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
