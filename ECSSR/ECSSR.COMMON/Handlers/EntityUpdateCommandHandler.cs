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
    public class EntityUpdateCommandHandler<TDbContext, TEntity, TKey, TUpdateModel, TReadModel>
       : DataContextHandlerBase<TDbContext, EntityUpdateCommand<TKey, TUpdateModel, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>
       where TDbContext : IECSSRDbContext
       where TEntity : class, IHaveIdentifier<TKey>, new()
    {
        public EntityUpdateCommandHandler(ILoggerFactory loggerFactory, IMapper mapper, TDbContext dataContext)
          : base(loggerFactory, dataContext, mapper)
        {

        }
        protected virtual async Task<TReadModel> Read(TKey key, CancellationToken cancellationToken = default(CancellationToken))
        {
            var model = await DataContext
                .Set<TEntity>()
                .AsNoTracking()
                .Where(p => Equals(p.Id, key))
                .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
            return model;
        }
        protected override async Task<EntityResponseModel<TReadModel>> ProcessAsync(EntityUpdateCommand<TKey, TUpdateModel, EntityResponseModel<TReadModel>> request, CancellationToken cancellationToken)
        {
            var entityResponse = new EntityResponseModel<TReadModel>();
            try
            {
                var dbSet = DataContext.Set<TEntity>();
                var keyValue = new object[] { request.Id };
                var entity = await dbSet.FindAsync(keyValue, cancellationToken).ConfigureAwait(false);

                if (entity == null) return default(EntityResponseModel<TReadModel>);
                Mapper.Map(request.Model, entity);
               
                await DataContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
                
                var readModel = await Read(entity.Id, cancellationToken);
                
                entityResponse.ReturnStatus = true;
                entityResponse.Data = readModel;
            }
            catch (Exception ex)
            {                
                entityResponse.ReturnMessage.Add(String.Format("Unable to Update Record {0}" + ex.Message, typeof(TEntity).Name));
                entityResponse.ReturnStatus = false;
            }
            return entityResponse;
        }
    }
}
