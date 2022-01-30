using Domain.ValueObjects;

namespace Domain.Interfaces;

public interface IPerson
{
    string FirstName { get; init; }
    string LastName { get; init; }
    SsnVO Ssn { get; init; }
}