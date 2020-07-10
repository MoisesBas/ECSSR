using System;
using System.Collections.Generic;
using System.Text;

namespace ECSSR.UTILITY.Interface
{
    public interface IRepositoryBase
    {
        void CreateIndex();
        void DeleteIndexIfExists();
    }
}
