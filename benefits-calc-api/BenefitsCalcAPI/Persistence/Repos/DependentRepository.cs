namespace Persistence;

public class DependentRepository: IAsyncRepository<DependentDto>
{
    private readonly IDatabase<DependentDto> _db;

    private const string InsertQuery =
        @"INSERT INTO Employees(FirstName, LastName, Ssn)
          Values(@FirstName, @LastName, @Ssn)
          RETURNING * ";

    public DependentRepository(IDatabase<DependentDto> db) => _db = db;

    public async Task<DependentDto> SaveAsync(DependentDto entity) =>
        await _db.QuerySingle(InsertQuery, entity);

    public async Task<IReadOnlyList<DependentDto>> GetAllAsync() => 
        (await _db.GetAll("Dependents")).ToImmutableList();
}