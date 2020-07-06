using System;
using System.Collections.Generic;
using System.Text;
using ECSSR.COMMON.Product.Dto;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;

namespace ECSSR.COMMON.ProductImage.Dto
{
    public class ProductImageReadDto: EntityModel<int>, ITrackCreated,ITrackUpdated
    {
        public string Title { get; set; }
        public byte[] ImageData { get; set; }
        public int? ProductId { get; set; }
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public ProductReadDto Product { get; set; }
    }
}
