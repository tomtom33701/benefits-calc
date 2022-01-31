namespace Domain.DTOs;

public class EmployeeDto
{
    public EmployeeDto(string? firstName, string? lastName, SsnVO ssn)
    {
        FirstName = firstName;
        LastName = lastName;
        Ssn = ssn;
    }

    public EmployeeDto()
    {
        
    }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Ssn { get; set; }
    public int? EmployeeId { get; set; }
}