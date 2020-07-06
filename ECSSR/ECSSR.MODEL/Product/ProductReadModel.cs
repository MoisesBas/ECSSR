using System;
using System.Collections.Generic;
using System.Text;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;

namespace ECSSR.MODEL.Product
{
    public class ProductReadModel : EntityModel<int>, ITrackCreated, ITrackUpdated
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
