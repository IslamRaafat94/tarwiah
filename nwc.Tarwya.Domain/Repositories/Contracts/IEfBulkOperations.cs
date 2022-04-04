using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nwc.Tarwya.Domain.Repositories.Contracts
{
    public interface IEFBulkOperations<TEntity> where TEntity : class
    {
        void BulkInsert(IList<TEntity> entities, BulkConfig bulkConfig = null);
        Task BulkInsertAsync(IList<TEntity> entities, BulkConfig bulkConfig = null);
        void BulkUpdate(IList<TEntity> entities, BulkConfig bulkConfig = null);
        Task BulkUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null);
        void BulkRemove(IList<TEntity> entities, BulkConfig bulkConfig = null);
        Task BulkRemoveAsync(IList<TEntity> entities, BulkConfig bulkConfig = null);
        void BulkTruncate(IList<TEntity> entities, BulkConfig bulkConfig = null);
        Task BulkTruncateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null);
        void EFBulkSaveChanges(BulkConfig bulkConfig = null);
        Task EFBulkSaveChangesAsync(BulkConfig bulkConfig = null);
    }
}
