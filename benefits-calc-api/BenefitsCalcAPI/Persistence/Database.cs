namespace Persistence;

public class Database<TDto> : IDatabase<TDto>
{
    private readonly SqliteConnection _connection;

    public Database(SqliteConnection connection)
    {
        _connection = connection;
    }

    public Task<TDto> QuerySingle(string query, TDto dto)
    {
        return _connection.QuerySingleAsync<TDto>(query, dto);
    }

    public Task<IEnumerable<TDto>> Filter(string query)
    {
        return _connection.QueryAsync<TDto>(query);
    }

    public Task<TDto> GetSingleOrDefault(string query, object @params)
    {
        return _connection.QuerySingleOrDefaultAsync<TDto>(query, @params);
    }
}