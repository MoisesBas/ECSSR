using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace ECSSR.COMMON.Commands
{
    public abstract class EntityIdentifierCommand<TKey, TReadModel>
          : IRequest<TReadModel>
    {
        public abstract string IncludeProperties { get; set; }
        protected EntityIdentifierCommand(TKey id)            
        {
            Id = id;
        }

        public TKey Id { get; }

    }
}
