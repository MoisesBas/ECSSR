using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECSSR.COMMON.Commands;
using ECSSR.COMMON.Handlers;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECSSR.COMMON.Handlers
{
    public class EntityIdentifierQueryHandler<TDbContext, TEntity, TKey, TReadModel>
       : DataContextHandlerBase<TDbContext, EntityIdentifierCommand<TKey, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>
       where TEntity : class, IHaveIdentifier<TKey>, new()
       where TDbContext : IECSSRDbContext
    {
       
        public EntityIdentifierQueryHandler(ILoggerFactory loggerFactory, TDbContext dataContext, IMapper mapper)
            : base(loggerFactory, dataContext, mapper)
        {
        }
        protected virtual async Task<TReadModel> Read(TKey key, string properties, CancellationToken cancellationToken = default(CancellationToken))
        {
            var model = DataContext
                .Set<TEntity>()
                .AsNoTracking()
                .Where(p => Equals(p.Id, key));

            if (!string.IsNullOrEmpty(properties))
            {
                model = properties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(model, (current, s) => current.Include(s.Trim(new char[] { ' ', '\n', '\r' })));
            }
            var result = model.FirstOrDefault();
            return Mapper.Map<TReadModel>(result);
        }
        protected override async Task<EntityResponseModel<TReadModel>> ProcessAsync(EntityIdentifierCommand<TKey, EntityResponseModel<TReadModel>> request, CancellationToken cancellationToken)
        {
            var entityResponse = new EntityResponseModel<TReadModel>();
            try
            {
                var model = await Read(request.Id, request.IncludeProperties, cancellationToken)
                    .ConfigureAwait(false);
                entityResponse.ReturnStatus = true;
                entityResponse.Data = model;
            }
            catch (Exception ex)
            {
                entityResponse.ReturnMessage.Add(String.Format("Unable to Get Record from {0} - with Id {1}" + ex.Message, typeof(TEntity).Name, request.Id.ToString()));
                entityResponse.ReturnStatus = false;
            }
            return entityResponse;
        }
    }
}
