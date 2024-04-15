using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Me.Talabt.Core.Entities;

namespace Me.Talabat.InfraStructure.Data
{
	public static class ApplicationContextSeed
	{
		public static async Task SeedAsync(ApplicationDbContext dbContext)
		{
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
			await dbContext.SaveChangesAsync();
		}
	}
}
