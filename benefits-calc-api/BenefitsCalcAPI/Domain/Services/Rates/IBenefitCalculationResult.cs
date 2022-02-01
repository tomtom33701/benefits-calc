namespace Domain.Services.Rates;

public interface IBenefitCalculationResult
{
    IPerson Person { get; }
    MoneyUsd AnnualBenefitCost { get; }
}