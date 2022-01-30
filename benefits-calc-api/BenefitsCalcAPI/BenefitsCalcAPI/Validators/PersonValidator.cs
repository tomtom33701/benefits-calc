using BenefitsCalcAPI.Models;
using Domain.ValueObjects;

namespace BenefitsCalcAPI.Validators;

public class PersonValidator: AbstractValidator<IPersonViewModel>
{
    public PersonValidator()
    {
        RuleFor(p => p.Ssn).Must(ssn => SsnVO.CheckValid(ssn));
        RuleFor(p => p.FirstName).NotNull().MaximumLength(50).MinimumLength(3);
        RuleFor(p => p.LastName).NotNull().MaximumLength(50).MinimumLength(3);
    }
}