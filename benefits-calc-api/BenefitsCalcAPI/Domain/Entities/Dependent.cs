namespace Domain.Entities;

public record Dependent(string FirstName, string LastName, SsnVO Ssn): IPerson;