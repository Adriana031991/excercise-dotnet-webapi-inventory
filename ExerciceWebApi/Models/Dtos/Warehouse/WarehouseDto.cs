namespace ExerciceWebApi.Models.Dtos
{
    public class WarehouseDto
    {
        public string WarehouseId { get; set; }

        public string WarehouseName { get; set; }

        public string WarehouseAddress { get; set; }


        //Relación con almacenamiento (StorageEntity)
        public ICollection<StorageDto>? Storages { get; set; }


    }
}