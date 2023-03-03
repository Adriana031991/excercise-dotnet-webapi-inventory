
namespace ExerciceWebApi.Models.Dtos
{
public class ProductDto
{
    public string ProductId { get; set; }
    public string ProductName { get; init; }
    public string ProductDescription { get; set; }

    public int TotalQuantity { get; set; }

    public string CategoryId { get; set; }

	public CategoryDto? Category { get; set; } 

}
}
