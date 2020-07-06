using System;
using System.Collections.Generic;
using System.Text;

namespace ECSSR.UTILITY.Interface
{
    public interface IHaveIdentifier<TKey>
    {
        TKey Id { get; set; }
    }
}
