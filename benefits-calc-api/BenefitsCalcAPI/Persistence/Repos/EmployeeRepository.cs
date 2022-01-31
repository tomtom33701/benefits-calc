namespace Persistence;

public class EmployeeRepository: IAsyncRepository<EmployeeDto>
{
    private readonly IDatabase<EmployeeDto> _db;

    public EmployeeRepository(IDatabase<EmployeeDto> db)
    {
        _db = db;
    }

    public async Task<EmployeeDto> SaveAsync(EmployeeDto entity)
    {
        return await _db.QuerySingle(
            "INSERT INTO Employees(FirstName, LastName, Ssn) Values(@FirstName, @LastName, @Ssn) RETURNING * ", entity);

    }

    public async Task<EmployeeDto> GetAsync(int id)
    {
        const string query = "select * from Employees where EmployeeId = @id";

        return await _db.GetSingleOrDefault(query, new { id });
    }

    public async Task<IReadOnlyList<EmployeeDto>> GetListAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<EmployeeDto>> GetAllAsync()
    {
        return (await _db.Filter("select * from Employees")).ToImmutableList();
    }
}