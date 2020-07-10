using System;
using System.Collections.Generic;
using System.Text;

namespace ECSSR.UTILITY.ElasticSearch
{
    public class ReIndexResponse
    {
        public bool Success { get; set; }
        public int TotalProcessed { get; set; }
        public ReIndexResponse(bool success = true)
        {
            Success = success;
            TotalProcessed = 0;
        }

        public ReIndexResponse MergeWith(ReIndexResponse other)
        {
            Success &= other.Success;
            TotalProcessed += other.TotalProcessed;
            return this;
        }
    }
}
