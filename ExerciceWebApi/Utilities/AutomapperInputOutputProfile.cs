using AutoMapper;
using ExerciceWebApi.Models.Dtos;
using ExerciceWebApi.Models.Entities;

public class AutomapperInputOutputProfile : Profile
{
    public AutomapperInputOutputProfile()
    {
        CreateMap<InputOutputDto, InputOutput>();
        CreateMap<InputOutput, InputOutputDto>();
    }
}