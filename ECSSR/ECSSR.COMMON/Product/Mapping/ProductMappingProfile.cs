using AutoMapper;
using ECSSR.COMMON.Product.Dto;

namespace ECSSR.COMMON.Product.Mapping
{
    public class ProductMappingProfile:Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductCreateDto, DOMAIN.Entities.Product>();
            CreateMap<ProductUpdateDto, DOMAIN.Entities.Product>();
            CreateMap<DOMAIN.Entities.Product, ProductReadDto>();
        }
    }
}
