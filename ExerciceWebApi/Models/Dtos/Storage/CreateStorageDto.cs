namespace ExerciceWebApi.Models.Dtos
{
    public class CreateStorageDto
    {

        public ProductDto Product { get; set; }

        public WarehouseDto Warehouse { get; set; }

        public int PartialQuantity { get; set; }

    }
}