using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace ECSSR.COMMON.Queries
{
    public class EntityListQuery<TReadModel>: IRequest<TReadModel>
    {
        public EntityListQuery()
            : this(null,(IEnumerable<EntitySort>)null)
        {
        }
        public EntityListQuery(EntityFilter filter)
           : this(filter, (IEnumerable<EntitySort>)null)
        {
        }
        public EntityListQuery(EntityFilter filter, EntitySort sort)
           : this(filter, new[] { sort })
        {
        }
        public EntityListQuery(EntityFilter filter, string includeProperties)
           : this(filter, (IEnumerable<EntitySort>)null)
        {
            this.IncludeProperties = includeProperties;
        }
        public EntityListQuery(EntityFilter filter, IEnumerable<EntitySort> sort)
           
        {
            Filter = filter;
            Sort = sort;
        }
        public EntityFilter Filter { get; set; }
        public IEnumerable<EntitySort> Sort { get; set; }
        public string IncludeProperties { get; set; }
    }
}
