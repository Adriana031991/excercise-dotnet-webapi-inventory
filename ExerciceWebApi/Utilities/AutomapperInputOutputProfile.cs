using AutoMapper;
using ExerciceWebApi.Models.Dtos;
using ExerciceWebApi.Models.Dtos.InputOutput;
using ExerciceWebApi.Models.Entities;

public class AutomapperInputOutputProfile : Profile
{
    public AutomapperInputOutputProfile()
    {
        CreateMap<CreateInputOutputDto, InputOutput>();
        CreateMap<InputOutputDto, InputOutput>();
        CreateMap<InputOutput, InputOutputDto>();
    }
}