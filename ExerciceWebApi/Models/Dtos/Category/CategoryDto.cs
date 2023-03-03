namespace ExerciceWebApi.Models.Dtos
{
    public class CategoryDto
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        //Relación con produtos (ProductEntity)
        public List<ProductDto>? Products { get; set; }
    }

}