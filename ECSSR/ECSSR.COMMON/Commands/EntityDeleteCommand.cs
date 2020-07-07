using System;
using System.Collections.Generic;
using System.Text;

namespace ECSSR.COMMON.Commands
{
    public class EntityDeleteCommand<TKey, TReadModel>
      : EntityIdentifierCommand<TKey, TReadModel>
    {
        public EntityDeleteCommand(TKey id) : base(id)
        {

        }
        public override string IncludeProperties { get; set; }
    }
}
