using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECSSR.COMMON.Handlers;
using ECSSR.COMMON.ProductImage.Dto;
using ECSSR.COMMON.Queries;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECSSR.COMMON.ProductImage.Handlers
{
    public class ProductImageGetByProductIdCommandHandler<TDbContext>
         : DataContextHandlerBase<TDbContext, EntityListQuery<EntityResponseListModel<ProductImageReadDto>>, EntityResponseListModel<ProductImageReadDto>>
         where TDbContext : IECSSRDbContext
    {
        public ProductImageGetByProductIdCommandHandler(ILoggerFactory loggerFactory, TDbContext dataContext, IMapper mapper) : base(loggerFactory, dataContext, mapper)
        {

        }
        protected override async Task<EntityResponseListModel<ProductImageReadDto>> ProcessAsync(EntityListQuery<EntityResponseListModel<ProductImageReadDto>> request, CancellationToken cancellationToken)
        {
            var entityResponse = new EntityResponseListModel<ProductImageReadDto>();
            try
            {
                var query = DataContext.Set<DOMAIN.Entities.ProductImage>().AsNoTracking();
                query = BuildQuery(request, query);
                var result = await EfQueryList(request, query, cancellationToken)
                                                            .ConfigureAwait(false);
                return result;
            }
            catch(Exception ex)
            {
                entityResponse.ReturnMessage.Add("Record not found");
                entityResponse.ReturnStatus = false;
            }
            return entityResponse;
        }
        protected virtual IQueryable<DOMAIN.Entities.ProductImage> BuildQuery(EntityListQuery<EntityResponseListModel<ProductImageReadDto>> request, IQueryable<DOMAIN.Entities.ProductImage> query)
        {
            if (request?.Filter != null)
                query = query.Filter(request.Filter);
            return query;
        }
        protected virtual async Task<EntityResponseListModel<ProductImageReadDto>> EfQueryList(EntityListQuery<EntityResponseListModel<ProductImageReadDto>> request, IQueryable<DOMAIN.Entities.ProductImage> query, CancellationToken cancellationToken)
        {
            var result = new EntityResponseListModel<ProductImageReadDto>();
            try
            {
                var data = await query
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
                result.Data = Mapper.Map<List<ProductImageReadDto>>(data);
                result.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                result.Errors.Add("GetAll", ex.Message);
                result.ReturnMessage.Add("Record not found");
                result.ReturnStatus = false;

            }
            return result;
        }
    }
}
