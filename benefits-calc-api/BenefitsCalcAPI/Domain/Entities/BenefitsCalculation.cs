namespace Domain.Entities;

public record BenefitsCalculation(BenefitRate EmployeeBenefitCost, 
    IImmutableList<(Dependent dependent, BenefitRate rate)> Dependents);