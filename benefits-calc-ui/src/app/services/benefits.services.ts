import { Injectable } from '@angular/core';
import { IBenefitsCalculation } from '@app/benefits/Models/benefits-calculation';
import { Employee, IEmployee } from '@app/benefits/Models/employee';
import { SsnValueObject } from '@app/benefits/Models/ValueObjects/ssn-value-object';
import { BehaviorSubject, Observable, of } from 'rxjs';

let mockData: IEmployee[] = [
  new Employee("John", "Smith", SsnValueObject.parse("231323232"), 100),
  new Employee("Adam", "Brown", SsnValueObject.parse("351765346"), 101),
  new Employee("Leslie", "Jones", SsnValueObject.parse("643539799"), 102),
  new Employee("Amanda", "Marcel", SsnValueObject.parse("567667697"), 103),
  new Employee("Tom", "Toups", SsnValueObject.parse("123456789"), 200),
  new Employee("John", "Smith", SsnValueObject.parse("987654321"), 201),
  new Employee("Alan", "Maher", SsnValueObject.parse("990939393"), 202),
];
const sub = new BehaviorSubject<IEmployee[]>(mockData);
@Injectable({
  providedIn: 'root'
})
export class BenefitsService {
  constructor() { }

  public getAllEmployees(): Observable<IEmployee[]> {
    return sub.asObservable();
  }
  public saveEmployee(employee: IEmployee): Observable<IEmployee> {
    const e: IEmployee = {
      ...employee,
      employeeId: generateId()
    };
    const emps = [...mockData.slice(0), e];
    sub.next(emps);
    return of(e);
  }
  public requestQuote(employee: IEmployee): Observable<IBenefitsCalculation> {
    return of(getQuote(employee));
  }
}
function generateId(): number {
  return Math.max(...mockData.map(x=>x.employeeId ?? 0)) + 1;
}
function getQuote(employee: IEmployee) {
  const quote: IBenefitsCalculation = {
    employeeBenefitCost: {
      annualBenefitCost: 1000,
      benefitCostPerPayPeriod: 1000/12
    },
    dependentCosts: []
  };
  employee.dependents?.forEach(d => {
    quote.dependentCosts.push([d, {
      annualBenefitCost: 500,
      benefitCostPerPayPeriod: 500/12
    }]);
  })
  return quote;
}
