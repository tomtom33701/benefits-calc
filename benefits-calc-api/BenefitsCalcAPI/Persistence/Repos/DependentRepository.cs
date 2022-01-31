namespace Persistence;

public class DependentRepository: IAsyncRepository<DependentDto>
{
    private readonly IDatabase<DependentDto> _db;

    private const string InsertQuery =
        @"INSERT INTO Dependents(FirstName, LastName, Ssn, EmployeeId)
          Values(@FirstName, @LastName, @Ssn, @EmployeeId)
          RETURNING * ";

    public DependentRepository(IDatabase<DependentDto> db) => _db = db;

    public async Task<DependentDto> SaveAsync(DependentDto entity) =>
        await _db.QuerySingle(InsertQuery, entity);

    public async Task<DependentDto> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<DependentDto>> GetListAsync(int id)
    {
        var query = $"select * from Dependents where EmployeeId ={id}";
        return (await _db.Filter(query)).ToImmutableList();
    }


    public async Task<IReadOnlyList<DependentDto>> GetAllAsync() => 
        (await _db.Filter("Select * from Dependents")).ToImmutableList();
}