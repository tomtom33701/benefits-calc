namespace Persistence;

public sealed class DependentRepository: AsyncRepository<DependentDto>
{
    private const string InsertQuery =
        @"INSERT INTO Dependents(FirstName, LastName, Ssn, EmployeeId)
          Values(@FirstName, @LastName, @Ssn, @EmployeeId)
          RETURNING * ";

    public DependentRepository(IDatabase db) : base(db)
    {
    }

    public override async Task<DependentDto> SaveAsync(DependentDto entity) =>
        await _db.QuerySingleAsync(InsertQuery, entity);

    public override Task<DependentDto> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public override async Task<IReadOnlyList<DependentDto>> GetListAsync(int id)
    {
        var query = $"select * from Dependents where EmployeeId ={id}";
        return (await _db.FilterAsync<DependentDto>(query)).ToImmutableList();
    }


    public override async Task<IReadOnlyList<DependentDto>> GetAllAsync() => 
        (await _db.FilterAsync<DependentDto>("Select * from Dependents")).ToImmutableList();


}