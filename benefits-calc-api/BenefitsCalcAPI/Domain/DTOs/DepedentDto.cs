namespace Domain.DTOs;

public class DependentDto
{
    public DependentDto(string? firstName, string? lastName, SsnVO ssn, int employeeId)
    {
        FirstName = firstName;
        LastName = lastName;
        Ssn = ssn;
        EmployeeId = employeeId;
    }

    public DependentDto()
    {
        
    }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Ssn { get; set; }
    public int EmployeeId { get; set; }

}