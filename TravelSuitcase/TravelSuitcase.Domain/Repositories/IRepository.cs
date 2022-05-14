namespace TravelSuitcase.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(int id, CancellationToken cancellationToken = default);

        Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}