namespace Domain.Queries;

public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, Employee?>
{
    private readonly IAsyncRepository<EmployeeDto> _empRepo;
    private readonly IAsyncRepository<DependentDto> _dependentRepo;


    public GetEmployeeByIdHandler(IAsyncRepository<EmployeeDto> empRepo, IAsyncRepository<DependentDto> dependentRepo)
    {
        _empRepo = empRepo;
        _dependentRepo = dependentRepo;
    }
    public async Task<Employee?> Handle(GetEmployeeByIdQuery request, CancellationToken _)
    {
        var dto = await _empRepo.GetAsync(request.Id);
        if (dto is null) return null;

        var dependents = await _dependentRepo.GetListAsync(dto.EmployeeId!.Value);
        return ToEmployee(dto, dependents);
    }
    
    private static Employee ToEmployee(EmployeeDto empDto, IEnumerable<DependentDto> depDto)
    {
        var dependents = depDto
            .Select(dDto => new Dependent(dDto.FirstName, dDto.LastName, dDto.Ssn!))
            .ToImmutableList();
        return new(empDto.EmployeeId!.Value, empDto.FirstName, empDto.LastName, empDto.Ssn!, dependents);
    }
}