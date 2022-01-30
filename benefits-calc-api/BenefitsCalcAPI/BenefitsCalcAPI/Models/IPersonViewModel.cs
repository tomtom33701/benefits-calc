namespace BenefitsCalcAPI.Models;

public interface IPersonViewModel
{
    string FirstName { get; init; }
    string LastName { get; init; }
    string Ssn { get; init; }
}