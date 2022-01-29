import { IPerson } from '@app/benefits/Models/person';
export interface IBenefitsCalculation {
  employeeId: number;
  employeeBenefitCost: IBenefitsRate;
  dependentCosts: Map<IPerson, IBenefitsRate>;
}
export interface IBenefitsRate {
  annualBenefitCost: number;
  benefitCostPerPayPeriod: number;
}
