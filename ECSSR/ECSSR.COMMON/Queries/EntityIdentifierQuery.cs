using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace ECSSR.COMMON.Queries
{
    public class EntityIdentifierQuery<TKey, TReadModel> : IRequest<TReadModel>
    {
        public EntityIdentifierQuery(TKey id)
            
        {
            Id = id;
        }
        public EntityIdentifierQuery( TKey id, string includeProperties)           
        {
            Id = id;
            IncludeProperties = includeProperties;
        }
        public TKey Id { get; }
        public string IncludeProperties { get; set; }
    }
}
