namespace BenefitsCalcAPI.Validators;

public class EmployeeValidator: AbstractValidator<EmployeeViewModel>
{
    public EmployeeValidator()
    {
        PersonValidator pv = new();
        RuleFor(e => e).SetValidator(pv);
        RuleForEach(e => e.Dependents).SetValidator(pv);
    }
}