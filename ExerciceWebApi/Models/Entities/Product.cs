namespace ExerciceWebApi.Models.Entities
{
    public class Product
	{
		public string ProductId { get; set; }

		public string ProductName { get; set; }

		public string ProductDescription { get; set; }

		public int? TotalQuantity { get; set; }


		//Relación con categorias (CategoriaEntity)
		public string CategoryId { get; set; }
		public Category? Category { get; set; } 


		//Relación con almacen (storageEntity)
		public ICollection<Storage>? Storages { get; set; }


		public override string ToString()
		{
			return "ProductId: " + ProductId + " ProductName: " + ProductName + " ProductDescription: " + ProductDescription +
				" CategoryId: " + CategoryId + " TotalQuantity: " + TotalQuantity;
		}

		public Product()
		{
		}

		public Product(string productId, string productName, string productDescription, int totalQuantity, string? categoryId, Category? category, ICollection<Storage>? storages)
		{
			ProductId = productId;
			ProductName = productName;
			ProductDescription = productDescription;
			TotalQuantity = totalQuantity;
			CategoryId = categoryId;
			Category = category;
			Storages = storages;
		}
	}
}