import { Component, OnInit } from '@angular/core';
import { Employee, IEmployee } from '@app/benefits/Models/employee';
import * as Actions from '@app/benefits/state/benefits.actions';
import { getEmployees, getError, IBenefitsState } from '@app/benefits/state/benefits.reducer';
import { Store } from '@ngrx/store';
import { map, Observable } from 'rxjs';
@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent implements OnInit {

  public employees$: Observable<IEmployee[]>;
  public error$: Observable<string>;

  constructor(private store: Store<IBenefitsState>) {
    this.employees$ = store.select(getEmployees).pipe(
      map(dtos => dtos.map(Employee.createFromDto))
    );
    this.error$ = store.select(getError);
  }

  ngOnInit(): void {
    this.store.dispatch(Actions.loadEmployees());
  }

  selectEmployee(employeeId: number): void {

    this.store.dispatch(Actions.setCurrentEmployee({ employeeId }));
    //to-do perform navigation
  }

  public get displayedColumns(): string[] {
    return ['id', 'name', 'last4Ssn', 'edit'];
  }
}
