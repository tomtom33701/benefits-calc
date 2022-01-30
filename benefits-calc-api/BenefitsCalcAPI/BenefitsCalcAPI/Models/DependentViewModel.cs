namespace BenefitsCalcAPI.Models;

public record DependentViewModel(string FirstName, string LastName, string Ssn): IPersonViewModel;