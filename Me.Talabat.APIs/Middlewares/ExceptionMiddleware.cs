
using System.Net;
using System.Text.Json;
using Me.Talabat.APIs.Errors;

namespace Me.Talabat.APIs.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
		{
			_next = next;
			_logger = logger;
			_env = env;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);

				context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
				context.Response.ContentType = "application/json";
				var response = _env.IsDevelopment() ? new ExceptionApiResponse(ex.Message, ex.StackTrace)
				:
				new ExceptionApiResponse(ex.Message);
				var jsonOptions = new JsonSerializerOptions()
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				};
				var Json =  JsonSerializer.Serialize(response,jsonOptions);
				await context.Response.WriteAsync(Json);

			}
		}

	}
}
