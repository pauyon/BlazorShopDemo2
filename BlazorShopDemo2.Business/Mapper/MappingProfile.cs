using AutoMapper;
using BlazorShopDemo2.DataAccess;
using BlazorShopDemo2.Models;

namespace BlazorShopDemo2.Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}