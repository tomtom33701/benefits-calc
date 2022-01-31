namespace Domain.Interfaces;

public interface IPerson
{
    string? FirstName { get; }
    string? LastName { get; }
    SsnVO Ssn { get; }
}