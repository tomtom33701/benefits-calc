namespace Domain.Entities;
public record BenefitRate(MoneyUsd AnnualBenefitCost, MoneyUsd BenefitCostPerPayPeriod, MoneyUsd BenefitCostPayPeriodRemainder);