using System;
using System.Collections.Generic;
using System.Text;

namespace ECSSR.COMMON.Commands
{
    public class EntityUpdateCommand<TKey, TUpdateModel, TReadModel>
        : EntityModelCommand<TUpdateModel, TReadModel>
    {
        public EntityUpdateCommand(TKey id, TUpdateModel model) : base(model)
        {
            Id = id;
        }
        public TKey Id { get; }
    }
}
