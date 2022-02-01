namespace Persistence;

public sealed class EmployeeRepository: AsyncRepository<EmployeeDto>
{

    public EmployeeRepository(IDatabase db) : base(db)
    {
    }

    public override async Task<EmployeeDto> SaveAsync(EmployeeDto entity)
    {
        return await _db.QuerySingleAsync(
            "INSERT INTO Employees(FirstName, LastName, Ssn) Values(@FirstName, @LastName, @Ssn) RETURNING * ", entity);

    }

    public override async Task<EmployeeDto> GetAsync(int id)
    {
        const string query = "select * from Employees where EmployeeId = @id";

        return await _db.GetSingleOrDefaultAsync<EmployeeDto>(query, new { id });
    }

    public override Task<IReadOnlyList<EmployeeDto>> GetListAsync(int id)
    {
        throw new NotImplementedException();
    }

    public override async Task<IReadOnlyList<EmployeeDto>> GetAllAsync()
    {
        return (await _db.FilterAsync<EmployeeDto>("select * from Employees")).ToImmutableList();
    }
}