namespace Domain.Services.Rates;

public interface IBenefitCalculationService
{
    BenefitsCalculation CalculateBenefits(Employee employee);
}