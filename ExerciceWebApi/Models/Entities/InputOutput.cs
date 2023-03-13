using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ExerciceWebApi.Models.Entities
{
    public class InputOutput
	{
		[Key]
		[StringLength(50)]
		[JsonProperty("InOutId")]

		public string InOutId { get; set; }


		[Required(ErrorMessage = "* El campo Fecha es obligatorio")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime InOutDate { get; set; }


		[Required(ErrorMessage = "* El campo Cantidad es obligatorio")]
		public int Quantity { get; set; }

		[Required]
		public bool IsInput { get; set; }


		//Relación con almacenamiento (StorageEntity)
		public string StorageId { get; set; }
		public Storage? Storage { get; set; }
		

		public override string ToString()
		{
			return "InOutId: " + InOutId + " InOutDate: " + InOutDate + " Quantity: " + Quantity +
				" IsInput: " + IsInput + " StorageId: " + StorageId;
		}

		public InputOutput()
		{
		}

		public InputOutput(string inOutId, DateTime inOutDate, int quantity, bool isInput, string storageId, Storage? storage)
		{
			InOutId = inOutId;
			InOutDate = inOutDate;
			Quantity = quantity;
			IsInput = isInput;
			StorageId = storageId;
			Storage = storage;
		}
	}
}