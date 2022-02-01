import { getBenefitQuote } from './../../state/benefits.reducer';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormControlStatus, FormGroup, FormGroupDirective, NgForm, ValidatorFn, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Employee, IEmployee } from '@app/benefits/Models/employee';
import { IPerson, Person } from '@app/benefits/Models/person';
import * as Actions from '@app/benefits/state/benefits.actions';
import { getError, IBenefitsState } from '@app/benefits/state/benefits.reducer';
import { Store } from '@ngrx/store';
import { debounceTime, Observable, Subscription, map } from 'rxjs';
import { IBenefitsCalculation } from '@app/benefits/Models/benefits-calculation';
import { Router } from '@angular/router';
import { IEmployeeDto } from '@app/benefits/Models/employee-dto';

class AddEmployeeStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
class Expressions {
  private constructor() { }
  public static readonly validNameExpression = /[A-Za-z]+(['/-]?[A-Za-z])*/g;
  public static readonly validSsnExpression = /^\d{3}-\d{2}-\d{4}$/g;
}

const nameValidations: ValidatorFn[] = [
  Validators.required,
  Validators.minLength(3),
  Validators.pattern(Expressions.validNameExpression)
];
@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent implements OnInit, OnDestroy {
  public formTitle: string;
  public employeeForm: FormGroup;
  public errorStateMatcher: ErrorStateMatcher = new AddEmployeeStateMatcher();
  public error$?: Observable<string>;
  public benefitCalculation$?: Observable<IBenefitsCalculation | null>;
  public employee: IEmployee;
  public dependents: FormArray;
  private dependentPersons: IPerson[] = [];
  private formStatusSubscription: Subscription;
  constructor(
    private fb: FormBuilder,
    private store: Store<IBenefitsState>,
    private router: Router
    ) {
    this.formTitle = "Add Employee";
    this.employee = new Employee();

    this.dependents = this.fb.array([]);
    this.employeeForm = this.fb.group({
      firstName: new FormControl(this.employee.firstName, nameValidations),
      lastName: new FormControl(this.employee.lastName, nameValidations),
      ssn: new FormControl(this.employee.ssn?.fullSsn, [
        Validators.pattern(Expressions.validSsnExpression),
        Validators.required
      ]),
      dependents: this.dependents
    });
    this.formStatusSubscription = this.employeeForm.statusChanges.pipe(
      debounceTime(800)
    ).subscribe(status => this.handleFormStatus(status));

  }
  ngOnDestroy(): void {
    this.formStatusSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this.error$ = this.store.select(getError);
    this.benefitCalculation$ = this.store.select(getBenefitQuote);

  }
  private handleFormStatus(status: FormControlStatus) {
    if(status === 'VALID') {
      this.store.dispatch(Actions.requestBenefitQuote({employee: this.formEmployeeDto}));
    }
  }
  public addDependent(): void {
    return this.dependents.push(this.buildDependent());
  }
  private buildDependent(): FormGroup {
    const person = new Person();
    this.dependentPersons.push(person);
    return this.fb.group({
      firstName: new FormControl(person.firstName, nameValidations),
      lastName: new FormControl(person.lastName, nameValidations),
      ssn: new FormControl(person.ssn?.fullSsn, [
        Validators.pattern(Expressions.validSsnExpression),
        Validators.required
      ]),
    });
  }
  private get formEmployeeDto(): IEmployeeDto {
    const getDigits = (str?: any) => (typeof str === 'string') && str.replace(/\D/g,'');
    return {
      ...this.employeeForm.value,
      ssn: getDigits(this.employeeForm.value.ssn),
      dependents: this.employeeForm.value.dependents?.map((d: { ssn: string; }) => ({
        ...d,
        ssn: getDigits(d.ssn)
      }))
    };
  }

  public save(): void {
    if (this.employeeForm.invalid) {
      return;
    }

    this.store.dispatch(Actions.saveEmployee({employee: this.formEmployeeDto}));
    this.router.navigate(['/benefits/employee-list']);
  }
}
