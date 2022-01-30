import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BenefitPriceComponent } from './benefit-price.component';

describe('BenefitPriceComponent', () => {
  let component: BenefitPriceComponent;
  let fixture: ComponentFixture<BenefitPriceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BenefitPriceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BenefitPriceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
