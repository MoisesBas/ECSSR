using System;
using System.Collections.Generic;
using System.Text;
using ECSSR.UTILITY.Interface;
using ECSSR.UTILITY.Model;
using Microsoft.AspNetCore.Http;

namespace ECSSR.COMMON.ProductImage.Dto
{
    public class ProductImageCreateDto : ITrackCreated
    {
        public string Title { get; set; }
        public IFormFile File { get; set; }
        public int? ProductId { get; set; }
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }
    }
}
