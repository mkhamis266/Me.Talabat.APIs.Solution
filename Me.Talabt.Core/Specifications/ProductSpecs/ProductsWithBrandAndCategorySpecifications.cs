using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Me.Talabt.Core.Entities;

namespace Me.Talabt.Core.Specifications.ProductSpecs
{
    public class ProductsWithBrandAndCategorySpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public ProductsWithBrandAndCategorySpecifications()
        {

        }

        public ProductsWithBrandAndCategorySpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
    }
}
