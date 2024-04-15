using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Me.Talabat.InfraStructure.Data;
using Me.Talabt.Core.Entities;
using Me.Talabt.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Me.Talabat.InfraStructure
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly ApplicationDbContext _dbContext;

		public GenericRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			if(typeof(T) == typeof(Product))
				return await _dbContext.Set<Product>().Include(p => p.Brand).Include(p => p.Category).ToListAsync() as IEnumerable<T>;
			
			return await _dbContext.Set<T>().ToListAsync<T>();
		}

		public async Task<T?> GetAsync(int id)
		{
			if (typeof(T) == typeof(Product))
				return await _dbContext.Set<Product>().Where(P => P.Id == id).Include(P=>P.Brand).Include(P=>P.Category).FirstOrDefaultAsync() as T;

			return await _dbContext.Set<T>().Where(P => P.Id == id).FirstOrDefaultAsync();
		}
	}
}
