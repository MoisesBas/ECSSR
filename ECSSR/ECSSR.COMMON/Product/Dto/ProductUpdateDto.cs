using System;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;

namespace ECSSR.COMMON.Product.Dto
{
    public class ProductUpdateDto : EntityModel<int>, ITrackUpdated
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
