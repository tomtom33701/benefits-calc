namespace Services;

public class BenefitCalculationService: IBenefitCalculationService
{
    private readonly IImmutableList<IRateCalculator> _calculators;
    private const decimal TotalAnnualPayPeriods = 26m;
    public BenefitCalculationService(IImmutableList<IRateCalculator> calculators)
    {
        _calculators = calculators;
    }
    public BenefitsCalculation CalculateBenefits(Employee employee)
    {
        var allPersons = employee.Dependents.Union(new[] { employee }).ToImmutableList();

        var tally = AggregateResults(allPersons).ToImmutableList();

        var employeeRate = GetBenefitRate(tally.First(x => x.Person is Employee));
        var dependents = tally.Where(x => x.Person is Dependent)
            .Select(x => ((Dependent)x.Person, GetBenefitRate(x)))
            .ToImmutableList();
        return new BenefitsCalculation(employeeRate, dependents);
    }

    private IEnumerable<IBenefitCalculationResult> AggregateResults(IEnumerable<IPerson> persons)
    {
        foreach (var person in persons)
        foreach (var calculator in _calculators)
        {
            if (calculator.CanAccept(person))
            {
                yield return calculator.CalculateRate(person);
            }
        }
    }

    private BenefitRate GetBenefitRate(IBenefitCalculationResult result)
    {
        MoneyUsd payPeriodAmount;
        payPeriodAmount = result.AnnualBenefitCost.DivideBy(TotalAnnualPayPeriods, out var payPeriodRemainder);
        return new BenefitRate(result.AnnualBenefitCost, payPeriodAmount, payPeriodRemainder);
    }
}
