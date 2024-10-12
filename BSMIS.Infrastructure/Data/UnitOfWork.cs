using BSIMS.Core.Interfaces;
using BSIMS.Infrastructure.Data;
using BSMIS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private bool _disposed = false;
    private readonly BSIMSDbContext _context;
    private readonly Dictionary<string, object> _repositories;
    private IDbContextTransaction _currentTransaction;

    public UnitOfWork(BSIMSDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<string, object>();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<TEntity>)_repositories[type];
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _currentTransaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_currentTransaction == null)
        {
            throw new InvalidOperationException("No transaction in progress. Call BeginTransaction first.");
        }

        try
        {
            await SaveChangesAsync();
            await _currentTransaction.CommitAsync();
            _currentTransaction = null; // Reset the transaction
        }
        catch (Exception ex)
        {
            await _currentTransaction.RollbackAsync();
            _currentTransaction = null; // Reset the transaction
            throw new Exception("An error occurred while committing the transaction.", ex);
        }
    }

    public void Rollback()
    {
        if (_currentTransaction != null)
        {
            _currentTransaction.Rollback();
            _currentTransaction = null; // Reset the transaction
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposed)
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
            }
            await _context.DisposeAsync();
            _disposed = true;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
