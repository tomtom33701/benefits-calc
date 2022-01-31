namespace BenefitsCalcAPI.Models;

public class EmployeeViewModel : IPersonViewModel
{
    public EmployeeViewModel(string firstName, string lastName, string ssn, int employeeId, IReadOnlyList<DependentViewModel> dependents)
    {
        FirstName = firstName;
        LastName = lastName;
        Ssn = ssn;
        EmployeeId = employeeId;
        Dependents = dependents;
    }

    private EmployeeViewModel(Employee entity)
    {
        FirstName = entity.FirstName!;
        LastName = entity.LastName!;
        Ssn = entity.Ssn;
        EmployeeId = entity.EmployeeId!.Value;
        Dependents = entity.Dependents.Select(d => new DependentViewModel(d.FirstName!, d.LastName!, d.Ssn)).ToImmutableList();
    }

    public static EmployeeViewModel CreateInstance(Employee entity) => new (entity);

    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Ssn { get; init; }
    public int EmployeeId { get; init; }
    public IReadOnlyList<DependentViewModel> Dependents { get; init; }

    public Employee ToEntity()
    {
        var dependents = Dependents.Select(dep =>
            new Dependent(dep.FirstName, dep.LastName, new SsnVO(dep.Ssn)))
            .ToImmutableList();
        return new Employee(null, FirstName, LastName, new SsnVO(Ssn), dependents);
    }
}