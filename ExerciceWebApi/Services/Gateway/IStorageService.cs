

using ExerciceWebApi.Models.Entities;

namespace ExerciceWebApi.Services.Gateway
{
    public interface IStorageService
    {
        Task<bool> IsProductInWarehouse(string storageId);
        Task<List<Storage>> StorageListByWarehouse(string idWarehouse);
    }
}