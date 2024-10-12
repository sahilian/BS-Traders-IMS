using BSIMS.Core.Interfaces;
using BSMIS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSIMS.Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BSIMSDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(BSIMSDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAllAsync()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity); // Just add the entity, no immediate save
            // No SaveChangesAsync() here
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity); // Mark the entity as modified
            // No SaveChangesAsync() here
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity); // Mark the entity for deletion
                // No SaveChangesAsync() here
            }
        }
    }
}
