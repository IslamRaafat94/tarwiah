using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using nwc.Tarwya.Domain.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace nwc.Tarwya.Domain.Repositories
{
	public interface IRepository<TEntity> :IEFBulkOperations<TEntity> where TEntity : class 
	{
		Task<IDbContextTransaction> GetTransactionAsync();
		IDbContextTransaction GetTransaction();
		EntityEntry Attach<T>(T entity);
		EntityEntry GetEntry<T>(T entity);
		void Add(TEntity entity);
		EntityEntry Entry(TEntity entity);
		Task AddAsync(TEntity entity);
		void Edit(TEntity entity);
		Task EditAsync(TEntity entity);
		void Delete(TEntity entityToDelete);
		Task DeleteAsync(TEntity entityToDelete);
		void Delete(object id);
		Task DeleteAsync(object id);
		TEntity GetById(object id);
		Task<TEntity> GetByIdAsync(object id);
		IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties);
		IEnumerable<TEntity> ExecuteRawSql(string query, params object[] parameters);
		int SaveChanges();
		Task<int> SaveChangesAsync();
	}
}
