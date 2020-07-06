using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;

namespace ECSSR.MODEL.Product
{
    public class ProductCreateModel : EntityModel<int>, ITrackCreated
    {
        public string Name { get; set; }
        public string Color { get; set; }     
        [JsonIgnore]
        public string CreatedBy { get; set; }
        [JsonIgnore]
        public DateTimeOffset Created { get; set; }
    }
}
