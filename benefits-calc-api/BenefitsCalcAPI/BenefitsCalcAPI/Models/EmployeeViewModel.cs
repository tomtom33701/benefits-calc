
namespace BenefitsCalcAPI.Models;

public record EmployeeViewModel(string FirstName, string LastName, string Ssn, List<IPersonViewModel> Dependents) : IPersonViewModel;