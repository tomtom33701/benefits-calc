namespace Persistence;

public class Database<TDto>
{
    private readonly SqliteConnection _connection;

    public Database(SqliteConnection connection)
    {
        _connection = connection;
    }

    public async Task<TDto> QuerySingleAsync<TDto>(string query, TDto dto)
    {
        _connection.QuerySingleAsync<TDto>()
    }
}