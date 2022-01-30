import { IBenefitsCalculation } from './../../Models/benefits-calculation';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'benefit-price',
  templateUrl: './benefit-price.component.html',
  styleUrls: ['./benefit-price.component.scss']
})
export class BenefitPriceComponent implements OnInit {
  private _benefitsCalculation?: IBenefitsCalculation | null;
  @Input()
  public set benefitsCalculation(value: IBenefitsCalculation | undefined | null) {
    this._benefitsCalculation = value;
  }
  public get benefitsCalculation() {
    return this._benefitsCalculation;
  }
  constructor() { }

  ngOnInit(): void {
  }
  public get annualTotal(): number {
    if(!this._benefitsCalculation) {
      return 0;
    }
    let totalAmount = this.benefitsCalculation?.employeeBenefitCost.annualBenefitCost ?? 0;
    this.benefitsCalculation?.dependentCosts?.forEach(value => totalAmount += value[1].annualBenefitCost);
    return totalAmount;
  }
  public get payPeriodTotal(): number {
    if(!this._benefitsCalculation) {
      return 0;
    }
    let totalAmount = this.benefitsCalculation?.employeeBenefitCost.benefitCostPerPayPeriod ?? 0;
    this.benefitsCalculation?.dependentCosts?.forEach(value => totalAmount += value[1].benefitCostPerPayPeriod);
    return totalAmount;
  }
}
