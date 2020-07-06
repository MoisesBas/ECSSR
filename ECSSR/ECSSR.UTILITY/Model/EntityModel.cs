using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ECSSR.UTILITY.Interface;

namespace ECSSR.UTILITY.Model
{
   
    public abstract class EntityModel<TKey> : IHaveIdentifier<TKey>
    {

        [Column(Order = 1)]
        public TKey Id { get; set; }

    }
}
