using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ECSSR.COMMON.Queries;
using ECSSR.UTILITY.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECSSR.COMMON.Handlers
{
   
    public class EntityPageQueryHandler<TDbContext, TEntity, TReadModel>
       : DataContextHandlerBase<TDbContext, EntityPagedQuery<EntityPagedResult<TReadModel>>, EntityPagedResult<TReadModel>>
       where TEntity : class
       where TDbContext : IECSSRDbContext
    {       

        public EntityPageQueryHandler(ILoggerFactory loggerFactory, TDbContext dataContext, IMapper mapper)
            : base(loggerFactory, dataContext, mapper)
        {
           
        }

        protected override async Task<EntityPagedResult<TReadModel>> ProcessAsync(EntityPagedQuery<EntityPagedResult<TReadModel>> request, CancellationToken cancellationToken)
        {
            var model = DataContext
                    .Set<TEntity>()
                    .AsNoTracking();

            if (!string.IsNullOrEmpty(request.IncludeProperties))
            {
                model = request.IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(model, (current, includeProperty) => current.Include(includeProperty));
            }
            // build query from filter
            model = BuildQuery(request, model);

            //get total for query


            var total = await QueryTotal(model, cancellationToken)
                .ConfigureAwait(false);

            //short circuit if total is zero
            if (total == 0)
                return new EntityPagedResult<TReadModel> { Data = new List<TReadModel>() };
            var data = model
               .Sort(request.Query.Sort)
               .Page(request.Query.Page, request.Query.PageSize).ToList();

            // page the query and convert to read model
            var result = Mapper.Map<List<TReadModel>>(data);

            return new EntityPagedResult<TReadModel>
            {
                Total = total,
                Data = result,
                Page = request.Query.Page,
                PageSize = request.Query.PageSize
            };
        }

        private static async Task<int> QueryTotal(IQueryable<TEntity> model, CancellationToken cancellationToken)
        {
            return await model
                .CountAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        private IQueryable<TEntity> BuildQuery(EntityPagedQuery<EntityPagedResult<TReadModel>> request, IQueryable<TEntity> model)
        {
            var entityQuery = request.Query;

            //build query from filter
            if (entityQuery?.Filter != null)
                model = model.Filter(entityQuery.Filter);

            //add raw query
            if (entityQuery != null && !string.IsNullOrEmpty(entityQuery.Query))
                model = model.Where(entityQuery.Query);


            return model;
        }
    }
}
