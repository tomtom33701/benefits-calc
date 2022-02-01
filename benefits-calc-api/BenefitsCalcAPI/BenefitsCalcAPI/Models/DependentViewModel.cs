namespace BenefitsCalcAPI.Models;

public record DependentViewModel(string FirstName, string LastName, string Ssn) : IPersonViewModel
{
    public static DependentViewModel FromEntity(Dependent dependent) => new(
        dependent.FirstName!, dependent.LastName!, dependent.Ssn.Ssn);
}