using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Me.Talabt.Core.Entities;

namespace Me.Talabt.Core.Specifications.ProductSpecs
{
	public class ProductsWithFiltersForCountSpecifications : BaseSpecifications<Product>
	{
		public ProductsWithFiltersForCountSpecifications(ProductSpecsParams specs) : base
			(
				P =>
					(
					 (!specs.BrandId.HasValue || P.BrandId == specs.BrandId) &&
					 (!specs.CategoryId.HasValue || P.CategoryId == specs.CategoryId) &&
					 (string.IsNullOrEmpty(specs.Search) || P.Name.ToLower().Contains(specs.Search))
					)
			)
		{

		}
	}
}
