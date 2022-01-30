namespace Domain.Entities;
public record Employee(
    int EmployeeId, 
    string FirstName, 
    string LastName, 
    SsnVO Ssn, 
    IReadOnlyList<IPerson> Dependents
): IPerson;