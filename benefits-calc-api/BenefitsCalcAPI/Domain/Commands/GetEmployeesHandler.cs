namespace Domain.Commands;

public class GetEmployeesHandler: IRequestHandler<GetEmployeesCommand, IReadOnlyList<Employee>>
{
    private readonly IAsyncRepository<EmployeeDto> _empRepo;
    private readonly IAsyncRepository<DependentDto> _dependentRepo;


    public GetEmployeesHandler(IAsyncRepository<EmployeeDto> empRepo, IAsyncRepository<DependentDto> dependentRepo)
    {
        _empRepo = empRepo;
        _dependentRepo = dependentRepo;
    }
    public async Task<IReadOnlyList<Employee>> Handle(GetEmployeesCommand request, CancellationToken _)
    {
        var employeeDtosTask = _empRepo.GetAllAsync();
        var dependentsDtosTask = _dependentRepo.GetAllAsync();
        await Task.WhenAll(employeeDtosTask, dependentsDtosTask);
        return JoinDtos(employeeDtosTask.Result, dependentsDtosTask.Result)
            .Select(r => ToEmployee(r.employee, r.dependents))
            .ToImmutableList();

    }

    private static IEnumerable<(EmployeeDto employee, IEnumerable<DependentDto> dependents)> JoinDtos(
        IEnumerable<EmployeeDto> eDtos, IEnumerable<DependentDto> dDtos) =>
        from e in eDtos
        join d in dDtos on e.EmployeeId equals d.EmployeeId
            into dependents
        select (Employee: e, Dependents: dependents);

    private static Employee ToEmployee(EmployeeDto empDto, IEnumerable<DependentDto> depDto)
    {
        var dependents = depDto
            .Select(dDto => new Dependent(dDto.FirstName, dDto.LastName, dDto.Ssn))
            .ToImmutableList();
        return new(empDto.EmployeeId!.Value, empDto.FirstName, empDto.LastName, empDto.Ssn, dependents);
    }
}