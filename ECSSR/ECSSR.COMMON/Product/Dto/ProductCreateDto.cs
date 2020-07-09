using System;
using System.Text.Json.Serialization;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;

namespace ECSSR.COMMON.Product.Dto
{
    public class ProductCreateDto : ITrackCreated
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }

        [JsonIgnore]
        public string CreatedBy { get; set; }
        [JsonIgnore]
        public DateTimeOffset Created { get; set; }
    }
}
