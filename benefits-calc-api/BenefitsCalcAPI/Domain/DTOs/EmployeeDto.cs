namespace Domain.DTOs;

public record EmployeeDto(
    string FirstName, 
    string LastName,
    string Ssn,
    int? EmployeeId = null);