namespace Domain.Repository;

public interface IAsyncRepository<TEntity> where TEntity : class
{
    Task<TEntity> SaveAsync(TEntity entity);
    Task<TEntity> GetAsync(int id);
    Task<IReadOnlyList<TEntity>> GetListAsync(int id);
    Task<IReadOnlyList<TEntity>> GetAllAsync();
}