
using AutoMapper;
using ExerciceWebApi.Models.Dtos;
using ExerciceWebApi.Models.Entities;

namespace ExerciceWebApi.Utilities
{
    public class AutomapperProductProfile : Profile
    {
        public AutomapperProductProfile(){
            CreateMap<CreateProductDto, Product>();
            CreateMap<Product , ProductDto>();
            CreateMap<ProductDto , Product>();
        }
    }
}