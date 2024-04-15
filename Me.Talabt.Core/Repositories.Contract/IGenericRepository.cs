using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Me.Talabt.Core.Entities;

namespace Me.Talabt.Core.Repositories
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		public Task<T?> GetAsync(int id);

		public Task<IEnumerable<T>> GetAllAsync();
    }
}
