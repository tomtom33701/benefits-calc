import { IPerson } from '@app/benefits/Models/person';
export interface IBenefitsCalculation {
  employeeBenefitCost: IBenefitsRate;
  dependentCosts: [IPerson, IBenefitsRate][];
}
export interface IBenefitsRate {
  annualBenefitCost: number;
  benefitCostPerPayPeriod: number;
}

