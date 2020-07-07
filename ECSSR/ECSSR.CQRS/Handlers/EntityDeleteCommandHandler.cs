using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ECSSR.COMMON.Commands;
using ECSSR.COMMON.Handlers;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECSSR.CQRS.Handlers
{
    public class EntityDeleteCommandHandler<TDbContext, TEntity, TKey, TReadModel>
       : DataContextHandlerBase<TDbContext, EntityDeleteCommand<TKey, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>
      where TDbContext : IECSSRDbContext
      where TEntity : class, IHaveIdentifier<TKey>, new()
    {
        public EntityDeleteCommandHandler(ILoggerFactory loggerFactory, IMapper mapper, TDbContext dataContext)
          : base(loggerFactory, dataContext, mapper)
        {

        }
        protected override async Task<EntityResponseModel<TReadModel>> ProcessAsync(EntityDeleteCommand<TKey, EntityResponseModel<TReadModel>> request, CancellationToken cancellationToken)
        {
            var entityResponse = new EntityResponseModel<TReadModel>();
            try
            {
                var dbSet = DataContext
                .Set<TEntity>();
                var keyValue = new object[] { request.Id };
                var entity = dbSet.Where(p => Equals(p.Id, request.Id));
                if (!string.IsNullOrEmpty(request.IncludeProperties))
                {
                    entity = request.IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(entity, (current, includeProperty) =>
                            current.Include(includeProperty.Trim(new char[] { ' ', '\n', '\r' })));
                }
                var model = entity.FirstOrDefault();
                if (model == null)
                    return default;

                dbSet.Remove(model).State = EntityState.Deleted;
                await DataContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
                var result = Mapper.Map<TReadModel>(model);               
                entityResponse.ReturnStatus = true;
                entityResponse.Data = result;
            }
            catch (Exception ex)
            {                
                entityResponse.ReturnMessage.Add(string.Format("Unable to Update Record {0}" + ex.Message, typeof(TEntity).Name));
                entityResponse.ReturnStatus = false;
            }
            return entityResponse;
        }
    }
}
