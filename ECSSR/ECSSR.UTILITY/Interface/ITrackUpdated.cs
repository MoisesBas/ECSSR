using System;
using System.Collections.Generic;
using System.Text;

namespace ECSSR.UTILITY.Interface
{
    public interface ITrackUpdated
    {
        public DateTimeOffset? Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
