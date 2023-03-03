namespace ExerciceWebApi.Models.Dtos.Response
{
	public class ResponseDto
	{
		public string Message {get;set;}
		public Object? Data {get;set;}

		public ResponseDto()
		{
		}

		public ResponseDto(string message)
		{
			Message = message;
		}

		public ResponseDto(string message, object? data)
		{
			Message = message;
			Data = data;
		}
	}
}
