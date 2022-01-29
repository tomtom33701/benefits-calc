import { Injectable } from '@angular/core';
import { Employee, IEmployee } from '@app/benefits/Models/employee';
import { SsnValueObject } from '@app/benefits/Models/ValueObjects/ssn-value-object';
import { Observable, of } from 'rxjs';

const mockData: IEmployee[] = [
  new Employee("John", "Smith", SsnValueObject.parse("231323232"), 100),
  new Employee("Adam", "Brown", SsnValueObject.parse("351765346"), 101),
  new Employee("Leslie", "Jones", SsnValueObject.parse("643539799"), 102),
  new Employee("Amanda", "Marcel", SsnValueObject.parse("567667697"), 103),
  new Employee("Tom", "Toups", SsnValueObject.parse("123456789"), 200),
  new Employee("John", "Smith", SsnValueObject.parse("987654321"), 201),
  new Employee("Alan", "Maher", SsnValueObject.parse("990939393"), 202),
];

@Injectable({
  providedIn: 'root'
})
export class BenefitsService {
  constructor() { }

  public getAllEmployees(): Observable<IEmployee[]> {
    return of(mockData);
  }
  public saveEmployee(employee: IEmployee): Observable<IEmployee> {
    employee.employeeId = generateId();
    mockData.push(employee);
    return of(employee);
  }
}
function generateId(): number {
  return Math.max(...mockData.map(x=>x.employeeId ?? 0)) + 1;
}
