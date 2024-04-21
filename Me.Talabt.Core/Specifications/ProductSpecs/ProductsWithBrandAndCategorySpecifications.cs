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
        public ProductsWithBrandAndCategorySpecifications(string sort)
        {
            if (sort == "priceAsc")
                AddOrderBy(P => P.Price);
            else if (sort == "priceDesc")
                AddOrderByDescending(P => P.Price);
            else if (sort == "nameDesc")
                AddOrderByDescending(P => P.Name);
            else
                AddOrderBy(P => P.Name);

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
	}
}
