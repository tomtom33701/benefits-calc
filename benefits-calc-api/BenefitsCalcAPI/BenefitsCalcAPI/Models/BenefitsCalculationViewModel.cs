namespace BenefitsCalcAPI.Models;
public record BenefitsCalculationViewModel(BenefitRateViewModel EmployeeBenefitCost,
    IList<Tuple<DependentViewModel,BenefitRateViewModel>> DependentCosts)
{
    public static BenefitsCalculationViewModel FromEntity(BenefitsCalculation entity)
    {
        var employeeRate = entity.EmployeeBenefitCost;
        var dependents = entity.Dependents;
        return new BenefitsCalculationViewModel(new BenefitRateViewModel(
                employeeRate.AnnualBenefitCost,
                employeeRate.BenefitCostPerPayPeriod,
                employeeRate.BenefitCostPayPeriodRemainder),
            dependents.Select(d => MapDependent(d.dependent, d.rate)).ToList());
    }

    private static Tuple<DependentViewModel, BenefitRateViewModel> MapDependent(Dependent dependent, BenefitRate rate)
    {
        return new (DependentViewModel.FromEntity(dependent), 
            new BenefitRateViewModel(
            rate.AnnualBenefitCost,
            rate.BenefitCostPerPayPeriod,
            rate.BenefitCostPayPeriodRemainder));
    }
}