using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Me.Talabt.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Me.Talabat.InfraStructure.Data.Configurations
{
	internal class ProductBrandConfigurations : IEntityTypeConfiguration<ProductBrand>
	{
		public void Configure(EntityTypeBuilder<ProductBrand> builder)
		{
			builder.Property(P => P.Name).IsRequired().HasMaxLength(50);
		}
	}
}
