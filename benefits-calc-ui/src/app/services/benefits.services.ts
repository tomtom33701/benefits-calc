import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBenefitsCalculation } from '@app/benefits/Models/benefits-calculation';
import { Employee, IEmployee } from '@app/benefits/Models/employee';
import { map, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BenefitsService {
  constructor(private http: HttpClient) { }
  private readonly baseUrl = "https://localhost:7093";
  private readonly employeesEndpoint = "employees";
  public getAllEmployees(): Observable<IEmployee[]> {
    return this.http.get<IEmployee[]>(`${this.baseUrl}/${this.employeesEndpoint}`).
      pipe(map(emps => emps.map(e=>Employee.create(e))));
  }
  public saveEmployee(employee: IEmployee): Observable<IEmployee> {
    return this.http.post<IEmployee>(`${this.baseUrl}/${this.employeesEndpoint}`,employee);
  }
  public requestQuote(employee: IEmployee): Observable<IBenefitsCalculation> {
    return of(getQuote(employee));
  }
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
