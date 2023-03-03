using System.ComponentModel.DataAnnotations;

namespace ExerciceWebApi.Models.Entities
{
    public class Storage
	{
		[Key]
		[StringLength(50)]
		public string StorageId { get; set; }

		[Required]
		public DateTime LastUpdate { get; set; }

		[Required]
		public int PartialQuantity { get; set; }


		//Relación con produtos (ProductEntity)
		public string ProductId { get; set; }
		public Product? Product { get; set; }


		//Relacion con bodegas (WherehouseEntity)
		public string WarehouseId { get; set; }
		public Warehouse? Warehouse { get; set; }


		//Relación con movimientos (InputOutputEntity)
		public IEnumerable<InputOutput>? InputOutputs { get; set; }

		public override string ToString()
		{
			return "StorageId: " + StorageId + " LastUpdate: " + LastUpdate + " PartialQuantity: " + PartialQuantity +
				" ProductId: " + ProductId + " WarehouseId: " + WarehouseId;
		}

		//public Storage()
		//{
		//}

		public Storage(int partialQuantity, Product? product, Warehouse? warehouse)
		{
			PartialQuantity = partialQuantity;
			Product = product;
			Warehouse = warehouse;
		}

		public Storage(string storageId, DateTime lastUpdate, int partialQuantity, string productId, Product? product, string warehouseId, Warehouse? warehouse, IEnumerable<InputOutput>? inputOutputs)
		{
			StorageId = storageId;
			LastUpdate = lastUpdate;
			PartialQuantity = partialQuantity;
			ProductId = productId;
			Product = product;
			WarehouseId = warehouseId;
			Warehouse = warehouse;
			InputOutputs = inputOutputs;
		}
	}
}