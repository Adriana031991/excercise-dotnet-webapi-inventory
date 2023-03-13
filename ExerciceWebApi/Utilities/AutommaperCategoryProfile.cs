using AutoMapper;
using ExerciceWebApi.Models.Dtos;
using ExerciceWebApi.Models.Entities;

public class AutomapperCategoryProfile : Profile
    {
        public AutomapperCategoryProfile(){
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category , CategoryDto>();
            CreateMap<CategoryDto , Category>();
        }
    }