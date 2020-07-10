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
using ECSSR.COMMON.ProductImage.Dto;
using ECSSR.COMMON.Queries;
using ECSSR.COMMON.ProductImage.Handlers;
using ECSSR.COMMON.Product.Handlers;

namespace ECSSR.COMMON
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainCommon(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddScoped<IRequestHandler<EntityPagedModelQuery<ProductSearchDto, ProductReadDto>, EntityPagedResult<ProductReadDto>>, ProductSearchCommandHandler<IECSSRDbContext>>();
            serviceCollection.TryAddScoped<IRequestHandler<EntityCreateCommand<ProductImageCreateDto, EntityResponseModel<ProductImageReadDto>>, EntityResponseModel<ProductImageReadDto>>, ProductImageCreateCommandHandler<IECSSRDbContext>>();
            serviceCollection.TryAddScoped<IRequestHandler<EntityListQuery<EntityResponseListModel<ProductImageReadDto>>, EntityResponseListModel<ProductImageReadDto>>, ProductImageGetByProductIdCommandHandler<IECSSRDbContext>>();            
            serviceCollection.AddEntityCommand<IECSSRDbContext, int, DOMAIN.Entities.Product, ProductCreateDto, ProductUpdateDto, ProductReadDto>();
            serviceCollection.AddEntityCommand<IECSSRDbContext, int, DOMAIN.Entities.ProductImage, ProductImageCreateDto, ProductImageUpdateDto, ProductImageReadDto>();
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
        

        private static IServiceCollection AddEntityCommand<TDbContext, TKey, TEntity, TCreateModel, TUpdateModel, TReadModel>(this IServiceCollection services)
      where TDbContext : IECSSRDbContext
      where TEntity : class, IHaveIdentifier<TKey>, new()
        {
            services.TryAddTransient<IRequestHandler<EntityPagedQuery<EntityPagedResult<TReadModel>>, EntityPagedResult<TReadModel>>, EntityPageQueryHandler<TDbContext, TEntity, TReadModel>>();
            services.TryAddTransient<IRequestHandler<EntityIdentifierQuery<TKey, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, EntityIdentifierQueryHandler<TDbContext, TEntity, TKey, TReadModel>>();
            services.TryAddTransient<IRequestHandler<EntityUpdateCommand<TKey, TUpdateModel, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, EntityUpdateCommandHandler<TDbContext, TEntity, TKey, TUpdateModel, TReadModel>>();
            services.TryAddTransient<IRequestHandler<EntityDeleteCommand<TKey, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, EntityDeleteCommandHandler<TDbContext, TEntity, TKey, TReadModel>>();
            services.TryAddTransient<IRequestHandler<EntityCreateCommand<TCreateModel, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, EntityCreateCommandHandler<TDbContext, TEntity, TKey, TCreateModel, TReadModel>>();
            return services;
        }
    }
}
