using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace ECSSR.COMMON.Commands
{
    public abstract class EntityModelCommand<TEntityModel, TReadModel> : IRequest<TReadModel>
    {
        protected EntityModelCommand(TEntityModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Model = model;
        }
        public TEntityModel Model { get; set; }

    }
}
