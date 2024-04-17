using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Me.Talabt.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Me.Talabat.InfraStructure.Data
{
	public class ApplicationDbContext: DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
			=> modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		DbSet<ProductCategory> ProductCategories { get; set; }
		DbSet<ProductBrand> ProductBrands { get; set; }
		DbSet<Product> Products { get; set;}

	}
}
