namespace BenefitsCalcAPI.Models;

public record BenefitRateViewModel(decimal annualBenefitCost, decimal benefitCostPerPayPeriod, decimal remainder);