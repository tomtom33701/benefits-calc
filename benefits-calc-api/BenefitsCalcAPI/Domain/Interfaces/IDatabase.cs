namespace Domain.Interfaces;

public interface IDatabase
{
    Task<TDto> QuerySingleAsync<TDto>(string query, TDto dto);
    Task<IEnumerable<TDto>> FilterAsync<TDto>(string query);
    Task<TDto> GetSingleOrDefaultAsync<TDto>(string query, object @params);
}