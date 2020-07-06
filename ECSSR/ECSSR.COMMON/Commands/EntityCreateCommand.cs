using System;
using System.Collections.Generic;
using System.Text;

namespace ECSSR.COMMON.Commands
{
    public class EntityCreateCommand<TCreateModel, TReadModel>
          : EntityModelCommand<TCreateModel, TReadModel>
    {
        public EntityCreateCommand(TCreateModel model) : base(model)
        {

        }
    }
}
