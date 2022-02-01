namespace Services.CalculationRules;

public class DependentRateCalculator: IRateCalculator
{

    public bool CanAccept(IPerson person) => person is Dependent;

    public IBenefitCalculationResult CalculateRate(IPerson person)
    {
        const decimal annualRate = 500m;
        var discountRate = QualifiesForDiscount(person) ? 0.9m : decimal.One;

        return new BenefitCalculationResult(person,  annualRate * discountRate);

    }

    private bool QualifiesForDiscount(IPerson person)
    {
        const string firstLetterForDiscount = "A";

        var firstNameLetter = person.FirstName![0].ToString();
        return string.Equals(firstNameLetter, firstLetterForDiscount, StringComparison.InvariantCultureIgnoreCase);
    }
}