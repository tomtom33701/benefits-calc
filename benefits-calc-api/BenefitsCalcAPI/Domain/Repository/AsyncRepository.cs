namespace Domain.Repository;

public abstract class AsyncRepository<TDto> : IAsyncRepository<TDto> where TDto : class
{
    protected readonly IDatabase _db;

    protected AsyncRepository(IDatabase db) => _db = db;

    public abstract Task<IReadOnlyList<TDto>> GetAllAsync();
    public abstract Task<TDto> GetAsync(int id);
    public abstract Task<IReadOnlyList<TDto>> GetListAsync(int id);
    public abstract Task<TDto> SaveAsync(TDto entity);
}