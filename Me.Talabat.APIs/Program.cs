
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Me.Talabat.InfraStructure.Data;
using Me.Talabt.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Me.Talabat.APIs
{
	public class Program
	{
		public static void Main(string[] args)
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
			#endregion

			
			using var app = webApplicationBuilder.Build();
			var scope = app.Services.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			var  LoggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
			try
			{
				dbContext.Database.Migrate();

			}catch (Exception ex)
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


			app.MapControllers();
			#endregion

			var productsFile = File.ReadAllText("../Me.Talabat.InfraStructure/Data/Data Seeding JSON Files/products.json");
			var products = JsonSerializer.Deserialize<List<Product>>(productsFile);

			if (products is not null && dbContext.Set<Product>().Count() == 0)
			{
				foreach (var item in products)
				{
					dbContext.Set<Product>().Add(item);
				}
			}

			var brandsFile = File.ReadAllText("../Me.Talabat.InfraStructure/Data/Data Seeding JSON Files/brands.json");
			var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsFile);

			if (brands is not null && dbContext.Set<ProductBrand>().Count() == 0)
			{
				foreach (var item in brands)
				{
					dbContext.Set<ProductBrand>().Add(item);
				}
			}

			var categoriesFile = File.ReadAllText("../Me.Talabat.InfraStructure/Data/Data Seeding JSON Files/categories.json");
			var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesFile);

			if (categories is not null && dbContext.Set<ProductCategory>().Count() == 0)
			{
				foreach (var item in categories)
				{
					dbContext.Set<ProductCategory>().Add(item);
				}
			}
			dbContext.SaveChanges();
			app.Run();
		}
	}
}
