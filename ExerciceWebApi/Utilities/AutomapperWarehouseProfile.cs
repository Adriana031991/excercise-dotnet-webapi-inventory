using AutoMapper;
using ExerciceWebApi.Models.Dtos;
using ExerciceWebApi.Models.Entities;

public class AutomapperWarehouseProfile : Profile
{
    public AutomapperWarehouseProfile()
    {
        CreateMap<CreateWarehouseDto, Warehouse>();
        CreateMap<Warehouse, WarehouseDto>();
        CreateMap<WarehouseDto, Warehouse>();
    }
}