namespace Domain.Repository;

public interface IAsyncRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetAsync(int id);
    Task<TEntity> SaveAsync(TEntity entity);
    Task<IReadOnlyList<TEntity>> GetAllAsync();
}