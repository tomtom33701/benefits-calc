
namespace Persistence;

public class EmployeeRepository: IAsyncRepository<EmployeeDto>
{
    private readonly SqliteConnection _connection;

    public EmployeeRepository(SqliteConnection connection)
    {
        _connection = connection;
    }
    public async Task<EmployeeDto> GetAsync(int id)
    {
        await _connection.ExecuteAsync()
    }

    public async Task<EmployeeDto> SaveAsync(EmployeeDto entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<EmployeeDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}