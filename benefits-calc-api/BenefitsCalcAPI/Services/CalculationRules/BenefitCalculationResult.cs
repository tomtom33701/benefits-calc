namespace Services.CalculationRules;



public record BenefitCalculationResult (IPerson Person, MoneyUsd AnnualBenefitCost) : IBenefitCalculationResult;