using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using nwc.Tarwya.Domain.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace nwc.Tarwya.Domain.Repositories
{
	public class EfRepository<TEntity> : IRepository<TEntity>, IEFBulkOperations<TEntity> where TEntity : class
	{
		private readonly DbContext db;
		private DbSet<TEntity> dbSet;

		public EfRepository(DbContext dbContext)
		{
			db = dbContext;
			dbSet = dbContext.Set<TEntity>();
		}
		public EntityEntry Attach<T>(T entity)
		{
			return db.Attach(entity);
		}
		public EntityEntry GetEntry<T>(T entity)
		{
			return db.Entry(entity);
		}
		public void Add(TEntity entity)
		{
			dbSet.Add(entity);
		}
		public EntityEntry Entry(TEntity entity)
		{
			return db.Entry(entity);
		}
		public async Task AddAsync(TEntity entity)
		{
			await dbSet.AddAsync(entity);
		}

		public void Edit(TEntity entity)
		{
			db.Entry(entity).State = EntityState.Modified;
		}

		public async Task EditAsync(TEntity entity)
		{
			await Task.Run(() => Edit(entity));
		}

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

		#region Save Changes
		public int SaveChanges()
		{
			return db.SaveChanges();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await db.SaveChangesAsync();

		}

        public void BulkInsert(IList<TEntity> entities, BulkConfig bulkConfig = null)
        {
			db.BulkInsert<TEntity>(entities, bulkConfig);
        }

        public async Task BulkInsertAsync(IList<TEntity> entities, BulkConfig bulkConfig = null)
        {
            await db.BulkInsertAsync<TEntity>(entities, bulkConfig);
		}

        public void BulkUpdate(IList<TEntity> entities, BulkConfig bulkConfig = null)
        {
			db.BulkUpdate<TEntity>(entities, bulkConfig);
		}

		public async Task BulkUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null)
        {
			await db.BulkUpdateAsync<TEntity>(entities, bulkConfig);
		}

        public void BulkRemove(IList<TEntity> entities, BulkConfig bulkConfig = null)
        {
			db.BulkDelete<TEntity>(entities, bulkConfig);
		}

        public async Task BulkRemoveAsync(IList<TEntity> entities, BulkConfig bulkConfig = null)
        {
            await db.BulkDeleteAsync<TEntity>(entities, bulkConfig);
		}

        public void BulkTruncate(IList<TEntity> entities, BulkConfig bulkConfig = null)
        {
			db.Truncate<TEntity>();
		}

        public async Task BulkTruncateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null)
        {
			await db.TruncateAsync<TEntity>();
		}

        public void EFBulkSaveChanges(BulkConfig bulkConfig = null)
        {
			db.BulkSaveChanges(bulkConfig);
        }

        public async Task EFBulkSaveChangesAsync(BulkConfig bulkConfig = null)
        {
			await db.BulkSaveChangesAsync(bulkConfig);
        }
        #endregion
    }
}
