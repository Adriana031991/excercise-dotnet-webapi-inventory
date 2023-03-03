using AutoMapper;
using ExerciceWebApi.Models.Dtos;
using ExerciceWebApi.Models.Entities;

public class AutoMapperStorageProfile : Profile
{
    public AutoMapperStorageProfile()
    {
        CreateMap<CreateStorageDto, Storage>();
        CreateMap<Storage, CreateStorageDto > ();
        CreateMap<Storage, StorageDto>();
        CreateMap<StorageDto, Storage>();
    }
}