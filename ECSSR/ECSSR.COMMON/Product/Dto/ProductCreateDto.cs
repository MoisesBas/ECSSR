using System;
using System.Text.Json.Serialization;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;

namespace ECSSR.COMMON.Product.Dto
{
    public class ProductCreateDto : EntityModel<int>, ITrackCreated
    {
        public string Name { get; set; }
        public string Color { get; set; }     
        [JsonIgnore]
        public string CreatedBy { get; set; }
        [JsonIgnore]
        public DateTimeOffset Created { get; set; }
    }
}
