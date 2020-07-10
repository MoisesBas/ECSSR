using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ECSSR.COMMON.Handlers;
using ECSSR.COMMON.Product.Dto;
using ECSSR.COMMON.Queries;
using ECSSR.UTILITY.ElasticSearch;
using ECSSR.UTILITY.Interface;
using Microsoft.Extensions.Logging;

namespace ECSSR.COMMON.Product.Handlers
{
    public class ProductSearchCommandHandler<TDbContext>
        : DataContextHandlerBase<TDbContext, EntityPagedModelQuery<ProductSearchDto, ProductReadDto>, EntityPagedResult<ProductReadDto>>
         where TDbContext : IECSSRDbContext
    {
        private readonly IProductRepository _productRepository;
        public ProductSearchCommandHandler(ILoggerFactory loggerFactory, TDbContext dataContext,
            IMapper mapper, IProductRepository productRepository) : base(loggerFactory, dataContext, mapper)
        {
            _productRepository = productRepository;
        }
        protected override async Task<EntityPagedResult<ProductReadDto>> ProcessAsync(EntityPagedModelQuery<ProductSearchDto, ProductReadDto> request, CancellationToken cancellationToken)
        {
            var entityResponse = new EntityPagedResult<ProductReadDto>();
            try
            {
                var filter = Mapper.Map<SearchRequest>(request.FilterModel);
                var result = await _productRepository.Search(filter);
            }
            catch(Exception ex)
            {

            }

            return entityResponse;
        }
    }
}
