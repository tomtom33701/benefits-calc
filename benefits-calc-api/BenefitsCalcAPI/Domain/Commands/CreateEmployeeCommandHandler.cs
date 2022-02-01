namespace Domain.Commands;

public class CreateEmployeeCommandHandler: IRequestHandler<CreateEmployeeCommand, Employee>
{
    private readonly IAsyncRepository<EmployeeDto> _employeeRepo;
    private readonly IAsyncRepository<DependentDto> _dependentRepo;
    private static readonly ParallelOptions ParallelOptions = new() { MaxDegreeOfParallelism =  2};
    public CreateEmployeeCommandHandler(IAsyncRepository<EmployeeDto> employeeRepo, IAsyncRepository<DependentDto> dependentRepo)
    {
        _employeeRepo = employeeRepo;
        _dependentRepo = dependentRepo;
    }
    public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = request.Employee;
        var savedEmployee = await _employeeRepo.SaveAsync(new(employee.FirstName, employee.LastName, employee.Ssn));
        var employeeId = savedEmployee.EmployeeId!.Value;

        if (!employee.Dependents.Any())
        {
            return new (employeeId,
                savedEmployee.FirstName,
                savedEmployee.LastName,
                employee.Ssn,
                employee.Dependents);
        }

        await Parallel.ForEachAsync(employee.Dependents.Select(dependent => ToDependentDto(dependent, employeeId)), 
            ParallelOptions, async (dependent, _) => await _dependentRepo.SaveAsync(dependent));


        return new (employeeId, savedEmployee.FirstName, savedEmployee.LastName, employee.Ssn, employee.Dependents);
    }
    private static DependentDto ToDependentDto(IPerson d, int employeeId) => new(d.FirstName, d.LastName, d.Ssn, employeeId);

}