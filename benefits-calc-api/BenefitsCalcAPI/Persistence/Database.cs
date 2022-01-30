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

    public Task<IEnumerable<TDto>> GetAll(string tableName)
    {
        return _connection.QueryAsync<TDto>($"SELECT * FROM {tableName}");
    }
}