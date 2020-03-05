using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Z.BulkOperations;

namespace nwc.Tarwya.Domain.Repositories
{
	public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private readonly DbContext db;
		private DbSet<TEntity> dbSet;

		public EfRepository(DbContext dbContext)
		{
			db = dbContext;
			dbSet = dbContext.Set<TEntity>();
		}

		#region Insert Methods
		public void Add(TEntity entity)
		{
			dbSet.Add(entity);
		}

		public async Task AddAsync(TEntity entity)
		{
			await dbSet.AddAsync(entity);
		}
		public void BulkAdd(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null)
		{
			dbSet.BulkInsert(entities, options);
		}
		public async Task BulkAddAsync(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null)
		{
			await dbSet.BulkInsertAsync(entities, options);
		}
		#endregion

		#region Update Methods
		public void Edit(TEntity entity)
		{
			db.Entry(entity).State = EntityState.Modified;
		}

		public async Task EditAsync(TEntity entity)
		{
			await Task.Run(() => Edit(entity));
		}
		public void BulkEdit(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null)
		{
			db.BulkUpdate(entities, options);
		}
		public async Task BulkEditAsync(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null)
		{
			await db.BulkUpdateAsync(entities, options);
		}
		#endregion

		#region Delete Methods

		public void Delete(TEntity entity)
		{
			dbSet.Remove(entity);
		}

		public void Delete(object id)
		{
			TEntity entity = GetById(id);
			if (entity == null)
				throw new NullReferenceException();
			Delete(entity);
		}

		public async Task DeleteAsync(TEntity entity)
		{
			await Task.Run(() => Delete(entity));
		}

		public async Task DeleteAsync(object id)
		{
			await Task.Run(() => Delete(id));
		}
		public void BulkDelete(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null)
		{
			db.BulkDelete(entities, options);
		}
		public async Task BulkDeleteAsync(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null)
		{
			await db.BulkDeleteAsync(entities, options);
		}
		#endregion

		#region Fetch Methods
		public IEnumerable<TEntity> ExecuteRawSql(string query, params object[] parameters)
		{
			throw new NotImplementedException();
		}

		public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
		{
			IQueryable<TEntity> query = dbSet;
			if (filter != null)
				query = query.Where(filter);

			if (includeProperties.Length > 0)
			{
				foreach (var i in includeProperties)
					query = query.Include(i);
			}

			return query;
		}

		public TEntity GetById(object id)
		{
			return dbSet.Find(id);
		}

		public async Task<TEntity> GetByIdAsync(object id)
		{
			return await dbSet.FindAsync(id);
		}
		#endregion

		#region Bulk Operations


		public void BulkMerge(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null)
		{
			db.BulkMerge(entities, options);
		}
		public async Task BulkMergeAsync(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null)
		{
			await db.BulkMergeAsync(entities, options);
		}
		public void BulkSynchronize(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null)
		{
			db.BulkSynchronize(entities, options);
		}
		public async Task BulkSynchronizeAsync(IEnumerable<TEntity> entities, Action<BulkOperation<TEntity>> options = null)
		{
			await db.BulkSynchronizeAsync(entities, options);
		}
		#endregion

		#region Save Changes
		public int SaveChanges()
		{
			return db.SaveChanges();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await db.SaveChangesAsync();

		}
		public void BulkSaveChanges(Action<BulkOperation> options = null)
		{
			db.BulkSaveChanges(options);
		}

		public async Task BulkSaveChangesAsync(Action<BulkOperation> options = null)
		{
			await db.BulkSaveChangesAsync(options);

		}
		#endregion
	}
}
