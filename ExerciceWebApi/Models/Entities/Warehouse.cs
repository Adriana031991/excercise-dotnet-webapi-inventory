using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ExerciceWebApi.Models.Entities
{
    public class Warehouse
	{
		[Key]
		[StringLength(50)]
		[JsonProperty("WarehouseId")]
		public string WarehouseId { get; set; }

		[Required(ErrorMessage = "* El campo Nombre es obligatorio")]
		[StringLength(100)]
		public string WarehouseName { get; set; }

		[Required]
		[StringLength(100)]
		public string WarehouseAddress { get; set; }


		//Relación con almacenamiento (StorageEntity)
		public ICollection<Storage>? Storages { get; set; }


		public override string ToString()
		{
			return "WarehouseId: " + WarehouseId + " WarehouseName: " + WarehouseName + " WarehouseAddress: " + WarehouseAddress +
				" Storages: " + Storages;
		}

		public Warehouse()
		{
		}


		public Warehouse(string warehouseId, string warehouseName, string warehouseAddress, ICollection<Storage>? storages)
		{
			WarehouseId = warehouseId;
			WarehouseName = warehouseName;
			WarehouseAddress = warehouseAddress;
			Storages = storages;
		}
	}
}