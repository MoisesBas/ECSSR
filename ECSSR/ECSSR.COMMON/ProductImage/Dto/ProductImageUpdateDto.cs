using System;
using System.Collections.Generic;
using System.Text;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;

namespace ECSSR.COMMON.ProductImage.Dto
{
    public class ProductImageUpdateDto : ITrackUpdated
    {
        public string Title { get; set; }
        public byte[] ImageData { get; set; }
        public int? ProductId { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
