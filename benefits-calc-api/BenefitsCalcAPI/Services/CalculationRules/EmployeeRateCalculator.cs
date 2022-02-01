namespace Services.CalculationRules;

public class EmployeeRateCalculator: IRateCalculator
{
    public bool CanAccept(IPerson person) => person is Employee;

    public IBenefitCalculationResult CalculateRate(IPerson person)
    {
        const decimal annualAmount = 1000m;
        var discountRateMultiplier = QualifiesForDiscount(person) ? 0.9m : 1m;

        return new BenefitCalculationResult(person,  annualAmount * discountRateMultiplier);

    }

    private bool QualifiesForDiscount(IPerson person)
    {
        const string firstLetterForDiscount = "A";

        var firstNameLetter = person.FirstName![0].ToString();
        return string.Equals(firstNameLetter, firstLetterForDiscount, StringComparison.InvariantCultureIgnoreCase);
    }
}