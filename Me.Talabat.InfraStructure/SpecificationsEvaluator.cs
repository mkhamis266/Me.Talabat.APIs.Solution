using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Me.Talabt.Core.Entities;
using Me.Talabt.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Me.Talabat.InfraStructure
{
	public static class  SpecificationsEvaluator<T> where T : BaseEntity
	{
		public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> specs)
		{
			var query = inputQuery;
			
			if (specs.Criteria is not null)
				query = query.Where(specs.Criteria);

			if(specs.OrderBy is not null)
				query = query.OrderBy(specs.OrderBy);

			if(specs.OrderByDescending is not null)
				query = query.OrderByDescending(specs.OrderByDescending);
			if (specs.IsPaginationEnabled)
				query = query.Skip(specs.Skip).Take(specs.Take);

			query = specs.Includes.Aggregate(query, (curruntQuery, includesExpression) => curruntQuery.Include(includesExpression));

			return query;
		}
	}
}
