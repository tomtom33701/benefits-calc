namespace Domain.Interfaces;

public interface IDatabase<TDto>
{
    Task<TDto> QuerySingle(string query, TDto dto);
    Task<IEnumerable<TDto>> GetAll(string tableName);
}