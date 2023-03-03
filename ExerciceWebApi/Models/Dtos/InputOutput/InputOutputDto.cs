namespace ExerciceWebApi.Models.Dtos

{

    public class InputOutputDto
    {
        public string InOutId { get; set; }

        public DateTime InOutDate { get; set; }

        public int Quantity { get; set; }

        public bool IsInput { get; set; }


        //Relación con almacenamiento (StorageEntity)
        public string StorageId { get; set; }
        public StorageDto? Storage { get; set; }

    }
}