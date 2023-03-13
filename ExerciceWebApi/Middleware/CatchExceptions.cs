using ExerciceWebApi.Models.Dtos.Exception;
using System.Net;
using System.Text.Json;

namespace ExerciceWebApi.Middleware
{
	public class CatchExceptions
	{
		private readonly RequestDelegate next;
		private readonly ILogger<CatchExceptions> logger;
		private readonly IHostEnvironment env;

		public CatchExceptions(RequestDelegate next, ILogger<CatchExceptions> logger, IHostEnvironment env)
		{
			this.next = next;
			this.logger = logger;
			this.env = env;
		}


		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await next(context);
			} catch (Exception ex)
			{

				logger.LogError(ex.Message, ex);
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				var response = env.IsDevelopment() ?
					new ExceptionDto(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) :
					new ExceptionDto(context.Response.StatusCode, "Interval Server Error");

				var json = JsonSerializer.Serialize(response);
				await context.Response.WriteAsync(json);
			
			}
		}
	}
}
