namespace Domain.Interfaces;

public interface IDatabase<TDto>
{
    Task<TDto> QuerySingle(string query, TDto dto);
    Task<IEnumerable<TDto>> Filter(string query);
    Task<TDto> GetSingleOrDefault(string query, object @params);
}