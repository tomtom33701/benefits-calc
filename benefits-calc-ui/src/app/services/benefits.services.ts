import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBenefitsCalculation } from '@app/benefits/Models/benefits-calculation';
import { IEmployeeDto } from '@app/benefits/Models/employee-dto';
import { Observable, of, tap, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BenefitsService {
  constructor(private http: HttpClient) { }
  //TO-Do: put url in a config file
  private readonly baseUrl = "https://localhost:7093";
  private readonly employeesEndpoint = "employees";
  private readonly ratesEndpoint = "benefits/rates";
  public getAllEmployees(): Observable<IEmployeeDto[]> {
    return this.http.get<IEmployeeDto[]>(`${this.baseUrl}/${this.employeesEndpoint}`);
  }
  public saveEmployee(employee: IEmployeeDto): Observable<IEmployeeDto> {
    return this.http.post<IEmployeeDto>(`${this.baseUrl}/${this.employeesEndpoint}`, employee);
  }
  public requestQuote(employee: IEmployeeDto): Observable<IBenefitsCalculation> {
    return this.http.post<IBenefitsCalculation>(`${this.baseUrl}/${this.ratesEndpoint}`, employee).pipe(
      map(calc => ({
        ...calc,
        dependentCosts: calc.dependentCosts.map((d: any) => [d.item1, d.item2])
      })
      )
    );
  }
}

