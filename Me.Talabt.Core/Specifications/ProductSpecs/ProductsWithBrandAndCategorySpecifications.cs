using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Me.Talabt.Core.Entities;

namespace Me.Talabt.Core.Specifications.ProductSpecs
{
	public class ProductsWithBrandAndCategorySpecifications : BaseSpecifications<Product>
	{
		public ProductsWithBrandAndCategorySpecifications(ProductSpecsParams specs) : base
			(
				P =>
					(
					 (!specs.BrandId.HasValue || P.BrandId == specs.BrandId) &&
					 (!specs.CategoryId.HasValue || P.CategoryId == specs.CategoryId) &&
					 (string.IsNullOrEmpty(specs.Search) || P.Name.ToLower().Contains(specs.Search))
					)
			)
		{
			if (specs.Sort == "priceAsc")
				AddOrderBy(P => P.Price);
			else if (specs.Sort == "priceDesc")
				AddOrderByDescending(P => P.Price);
			else if (specs.Sort == "nameDesc")
				AddOrderByDescending(P => P.Name);
			else
				AddOrderBy(P => P.Name);

			ApplyPagination(specs.PageSize, (specs.PageIndex - 1) * specs.PageSize);

			AddIncludes();
		}
		public ProductsWithBrandAndCategorySpecifications(int id) : base(P => P.Id == id)
		{
			AddIncludes();
		}

		private void AddIncludes()
		{
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);
		}
		public void AddOrderBy(Expression<Func<Product, object>> orderByExpression)
		{
			OrderBy = orderByExpression;
		}
		public void AddOrderByDescending(Expression<Func<Product, object>> orderByExpressionDescending)
		{
			OrderByDescending = orderByExpressionDescending;
		}

		public void ApplyPagination(int take, int skip)
		{
			IsPaginationEnabled = true;
			Take = take;
			Skip = skip;
		}
	}
}
