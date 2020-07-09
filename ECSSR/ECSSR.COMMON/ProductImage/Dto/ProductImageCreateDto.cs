using System;
using System.Text.Json.Serialization;
using ECSSR.UTILITY.Interface;
using Microsoft.AspNetCore.Http;

namespace ECSSR.COMMON.ProductImage.Dto
{
    public class ProductImageCreateDto : ITrackCreated
    {
        public string Title { get; set; }
        public IFormFile File { get; set; }
        public int? ProductId { get; set; }
        [JsonIgnore]
        public DateTimeOffset Created { get; set; }
        [JsonIgnore]
        public string CreatedBy { get; set; }
    }
}
