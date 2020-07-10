using System;
using System.Collections.Generic;
using System.Text;

namespace ECSSR.COMMON.Product.Dto
{
    public class ProductSearchDto
    {

        public string Name { get; set; }
        public string Color { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
