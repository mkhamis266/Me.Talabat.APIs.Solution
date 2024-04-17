
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Me.Talabat.APIs.Errors;
using Me.Talabat.APIs.Helpers;
using Me.Talabat.InfraStructure;
using Me.Talabat.InfraStructure.Data;
using Me.Talabt.Core.Entities;
using Me.Talabt.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Me.Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			#region Services Configuartions
			webApplicationBuilder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();
			webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options => {
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("defaultConnection"));
			});
			webApplicationBuilder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			webApplicationBuilder.Services.AddAutoMapper(typeof(MappingProfiles));

			webApplicationBuilder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) => 
				{
					var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count >0)
															.SelectMany(P=>P.Value.Errors)
															.Select(P=> P.ErrorMessage)
															.ToArray();
					var response = new ValidationErrorApiResponse()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(response);
				};
			});
			#endregion

			
			using var app = webApplicationBuilder.Build();
			var scope = app.Services.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			var  LoggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
			try
			{
				await dbContext.Database.MigrateAsync();
				await ApplicationContextSeed.SeedAsync(dbContext);
			}
			catch (Exception ex)
			{
				var logger = LoggerFactory.CreateLogger<Program>();
				logger.LogError(ex.Message, "an error occured while updating database");
			}
			#region Cinfigurations
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseStaticFiles();
			app.MapControllers();
			#endregion

			
			app.Run();
		}
	}
}
