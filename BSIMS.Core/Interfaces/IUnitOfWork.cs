using System;
using System.Threading.Tasks;

namespace BSIMS.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync(); // New method to begin a transaction
        Task CommitAsync(); // Commit changes in the current transaction
        void Rollback(); // Rollback the current transaction
    }
}
