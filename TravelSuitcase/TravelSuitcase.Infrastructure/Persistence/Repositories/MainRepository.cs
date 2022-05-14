using Microsoft.EntityFrameworkCore;
using TravelSuitcase.Domain.Common.Entities;
using TravelSuitcase.Domain.Repositories;

namespace TravelSuitcase.Infrastructure.Persistence.Repositories
{
    public class MainRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected DbContext DbContext { get; }
        public DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

        public MainRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            TEntity entity = await GetByIdAsync(id, cancellationToken);
            DbSet.Remove(entity);
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await DbSet.AnyAsync(x => x.Equals(entity), cancellationToken);
        }

        public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DbSet.AnyAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public async Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}