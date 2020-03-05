using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Z.BulkOperations;

namespace nwc.Tarwya.Domain.Repositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		void Add(TEntity entity);
		Task AddAsync(TEntity entity);
		void BulkAdd(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null);
		Task BulkAddAsync(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null);
		void Edit(TEntity entity);
		Task EditAsync(TEntity entity);
		void BulkEdit(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null);
		Task BulkEditAsync(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null);
		void Delete(TEntity entityToDelete);
		Task DeleteAsync(TEntity entityToDelete);
		void Delete(object id);
		Task DeleteAsync(object id);
		void BulkDelete(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null);
		Task BulkDeleteAsync(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null);
		TEntity GetById(object id);
		Task<TEntity> GetByIdAsync(object id);
		IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties);
		IEnumerable<TEntity> ExecuteRawSql(string query, params object[] parameters);
		void BulkMerge(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null);
		Task BulkMergeAsync(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null);
		void BulkSynchronize(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null);
		Task BulkSynchronizeAsync(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null);
		int SaveChanges();
		Task<int> SaveChangesAsync();
		void BulkSaveChanges(Action<BulkOperation> options = null);
		Task BulkSaveChangesAsync(Action<BulkOperation> options = null);

	}
}
