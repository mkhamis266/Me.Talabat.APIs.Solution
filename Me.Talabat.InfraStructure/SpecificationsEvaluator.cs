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

			query = specs.Includes.Aggregate(query, (curruntQuery, includesExpression) => curruntQuery.Include(includesExpression));

			return query;
		}
	}
}
