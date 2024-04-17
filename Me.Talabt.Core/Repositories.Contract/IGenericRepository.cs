using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Me.Talabt.Core.Entities;
using Me.Talabt.Core.Specifications;

namespace Me.Talabt.Core.Repositories
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		public Task<T?> GetAsync(int id);

		public Task<IEnumerable<T>> GetAllAsync();

		public Task<T?> GetWithSpecAsync(ISpecifications<T> specs);

		public Task<IEnumerable<T>> GetAllWithSpecsAsync(ISpecifications<T> specs);
	}
}
