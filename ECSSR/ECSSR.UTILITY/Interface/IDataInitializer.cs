using System;
using System.Collections.Generic;
using System.Text;

namespace ECSSR.UTILITY.Interface
{
    public interface IDataInitializer
    {
        void Initialize();
        int PerformIndexing(int batchSize, int batchSkip = 0);
    }
}
