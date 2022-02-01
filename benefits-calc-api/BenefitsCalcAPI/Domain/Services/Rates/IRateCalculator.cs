namespace Domain.Services.Rates;

public interface IRateCalculator
{
    bool CanAccept(IPerson person);
    IBenefitCalculationResult CalculateRate(IPerson person);
}