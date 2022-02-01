import { IPersonDto } from './employee-dto';
export interface IBenefitsCalculation {
  employeeBenefitCost: IBenefitsRate;
  dependentCosts: [IPersonDto, IBenefitsRate][];
}
export interface IBenefitsRate {
  annualBenefitCost: number;
  benefitCostPerPayPeriod: number;
  remainder: number;
}

