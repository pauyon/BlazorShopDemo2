using AutoMapper;
using BlazorShopDemo2.Domain.Entities;
using BlazorShopDemo2.Domain.Models;

namespace BlazorShopDemo2.Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductPrice, ProductPriceDto>().ReverseMap();
        }
    }
}