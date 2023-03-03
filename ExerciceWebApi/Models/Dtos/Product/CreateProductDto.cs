namespace ExerciceWebApi.Models.Dtos
{

public class CreateProductDto
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }

    public int TotalQuantity { get; set; }

    public string CategoryId { get; set; }

}
}