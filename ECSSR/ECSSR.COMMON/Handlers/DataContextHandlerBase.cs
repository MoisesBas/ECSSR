using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ECSSR.UTILITY.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECSSR.COMMON.Handlers
{
    public abstract class DataContextHandlerBase<TDbContext, TRequest, TResponse>
       : RequestHandlerBase<TRequest, TResponse>
       where TDbContext : IECSSRDbContext
       where TRequest : IRequest<TResponse>
    {
        protected DataContextHandlerBase(ILoggerFactory loggerFactory, TDbContext dataContext, IMapper mapper)
            : base(loggerFactory)
        {
            
            DataContext = dataContext;
            Mapper = mapper;
        }
        protected TDbContext DataContext { get; }

        protected IMapper Mapper { get; }
    }
}
