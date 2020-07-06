using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ECSSR.COMMON.ProductImage.Dto;

namespace ECSSR.COMMON.ProductImage.Mapping
{
   public class ProductImageMappingProfile:Profile
    {
        public ProductImageMappingProfile()
        {
            CreateMap<ProductImageCreateDto, DOMAIN.Entities.ProductImage>();
            CreateMap<ProductImageUpdateDto, DOMAIN.Entities.ProductImage>();
            CreateMap<DOMAIN.Entities.ProductImage, ProductImageReadDto>();
        }
    }
}
