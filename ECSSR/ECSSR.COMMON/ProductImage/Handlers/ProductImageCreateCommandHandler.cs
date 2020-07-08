using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ECSSR.COMMON.Commands;
using ECSSR.COMMON.Handlers;
using ECSSR.COMMON.ProductImage.Dto;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;
using Microsoft.Extensions.Logging;

namespace ECSSR.COMMON.ProductImage.Handlers
{
    public class ProductImageCreateCommandHandler<TDbContext>
           : DataContextHandlerBase<TDbContext, EntityCreateCommand<ProductImageCreateDto, EntityResponseModel<ProductImageReadDto>>, EntityResponseModel<ProductImageReadDto>>
       where TDbContext : IECSSRDbContext
    {
        public ProductImageCreateCommandHandler(ILoggerFactory loggerFactory, IMapper mapper, TDbContext dataContext)
           : base(loggerFactory, dataContext, mapper)
        {

        }
        protected override async Task<EntityResponseModel<ProductImageReadDto>> ProcessAsync(EntityCreateCommand<ProductImageCreateDto, EntityResponseModel<ProductImageReadDto>> request, CancellationToken cancellationToken)
        {
            EntityResponseModel<ProductImageReadDto> response = new EntityResponseModel<ProductImageReadDto>();
            try
            {
                var current = Mapper.Map<DOMAIN.Entities.ProductImage>(request.Model);
                var files = new byte[] { };
                if (request.Model.File == null) throw new Exception("Product Image is required.,");               
                if (request.Model.File != null)
                {
                    if (request.Model.File.Length > 0)
                    {
                        var file = request.Model.File;
                        using var ms = new MemoryStream();
                        file.CopyTo(ms);
                        current.ImageData = ms.ToArray();
                    }
                }
               
                    var dbSet = DataContext.Set<DOMAIN.Entities.ProductImage>();
                    await dbSet
                        .AddAsync(current, cancellationToken)
                        .ConfigureAwait(false);
                    await DataContext.SaveChangesAsync(cancellationToken)
                                     .ConfigureAwait(false);              

                    response.ReturnStatus = true;
                    response.Data = Mapper.Map<ProductImageReadDto>(current);
               
            }
            catch (Exception ex)
            {
                response.ReturnMessage.Add(String.Format("Unable to Insert Record {0}" + ex.Message, typeof(DOMAIN.Entities.ProductImage).Name));
                response.ReturnStatus = false;               
            }
            return response;
        }
    }
}
