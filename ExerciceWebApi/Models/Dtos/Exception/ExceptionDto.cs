namespace ExerciceWebApi.Models.Dtos.Exception
{
	public class ExceptionDto
	{
		private readonly int statusCode;
		private readonly string message;
		private readonly string details;

		public ExceptionDto(int statusCode, string message = null , string details=null)
		{
			this.statusCode = statusCode;
			this.message = message;
			this.details = details;
		}
	}
}
