using System;
using System.Collections.Generic;
using System.Text;

namespace ECSSR.UTILITY.Interface
{
    public interface ITrackCreated
    {
        DateTimeOffset Created { get; set; }
        string CreatedBy { get; set; }
    }
}
