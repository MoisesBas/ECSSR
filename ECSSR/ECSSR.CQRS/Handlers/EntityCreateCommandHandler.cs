using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ECSSR.COMMON.Commands;
using ECSSR.COMMON.Handlers;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;
using Microsoft.Extensions.Logging;

namespace ECSSR.CQRS.Handlers
{
    public class EntityCreateCommandHandler<TDbContext, TEntity, TKey, TCreateModel, TReadModel>
       : DataContextHandlerBase<TDbContext, EntityCreateCommand<TCreateModel, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>
       where TDbContext : IECSSRDbContext
       where TEntity : class, IHaveIdentifier<TKey>, new()
    {
        public EntityCreateCommandHandler(ILoggerFactory loggerFactory, IMapper mapper, TDbContext dataContext)
           : base(loggerFactory, dataContext, mapper)
        {

        }
        protected override async Task<EntityResponseModel<TReadModel>> ProcessAsync(EntityCreateCommand<TCreateModel, EntityResponseModel<TReadModel>> request, CancellationToken cancellationToken)
        {
            var EntityResponse = new EntityResponseModel<TReadModel>();
            var dbSet = DataContext.Set<TEntity>();
            var entiy = Mapper.Map<TEntity>(request.Model);
            dbSet.Add(entiy);
            await DataContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
            var readModel = Mapper.Map<TReadModel>(entiy);
            EntityResponse.ReturnStatus = true;
            EntityResponse.Data = readModel;
            return EntityResponse;
        }
    }
}
