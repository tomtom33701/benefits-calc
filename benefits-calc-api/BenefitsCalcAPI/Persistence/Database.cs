using Domain.Exceptions;

namespace Persistence;

public class Database : IDatabase
{
    private readonly string _connectionString;
    private readonly ILogger _logger;

    public Database(string connectionString, ILogger<Database> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public Task<TDto> QuerySingleAsync<TDto>(string query, TDto dto) => 
        WithConnection(c=> c.QuerySingleAsync<TDto>(query, dto));
    

    public Task<IEnumerable<TDto>> FilterAsync<TDto>(string query) => 
        WithConnection(c => c.QueryAsync<TDto>(query));

    public Task<TDto> GetSingleOrDefaultAsync<TDto>(string query, object @params) =>
        WithConnection(async c => await c.QuerySingleOrDefaultAsync<TDto>(query, @params)); 

    private async Task<T> WithConnection<T>(Func<SqliteConnection, Task<T>> withFunc)
    {
        try
        {
            await using var connection = new SqliteConnection(_connectionString);
            return await withFunc(connection);
        }
        catch (SqliteException ex)
        {
            _logger.LogError("Database error has occurred", ex);
            throw new PersistenceException("Sqlite Database threw and exception", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError("Non-specific exception occurred executing a query", ex);
            throw;
        }
    }
}