import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, ValidatorFn, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { IPerson, Person} from '@app/benefits/Models/person';
import { Employee, IEmployee } from '@app/benefits/Models/employee';


/** Error when invalid control is dirty, touched, or submitted. */
class AddEmployeeStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
class Expressions {
  private constructor(){}
  public static readonly validNameExpression = /[A-Za-z]+(['/-][A-Za-z])*/g;
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
export class EmployeeComponent implements OnInit {
  public formTitle: string;
  public employeeForm: FormGroup;
  public errorStateMatcher: ErrorStateMatcher = new AddEmployeeStateMatcher();

  public employee: IEmployee;
  private dependentPersons: IPerson[] = [];
  constructor(private fb: FormBuilder) {
    this.formTitle = "Add Employee";
    this.employee = new Employee();

    this.employeeForm = this.fb.group({
      firstName: new FormControl(this.employee.firstName, nameValidations),
      lastName: new FormControl(this.employee.lastName, nameValidations),
      ssn: new FormControl(this.employee.ssn?.fullSsn, [
        Validators.pattern(Expressions.validSsnExpression),
        Validators.required
      ]),
      dependents: this.fb.array([])
    });

  }
  public get dependents(): FormArray {
    return this.employeeForm.get('dependents') as FormArray;
  }
  public get firstName(): AbstractControl {
    return this.employeeForm.get('firstName')!;
  }
  public get lastName(): AbstractControl {
    return this.employeeForm.get('lastName')!;
  }
  public get ssn(): AbstractControl {
    return this.employeeForm.get('ssn')!;
  }
  public addDependent(): void {
    return this.dependents.push(this.buildDependent());
  }
  public isEmployeeValid(): boolean {
    for (const c of [this.firstName,this.lastName,this.ssn]) {
      if(c.invalid) return false;
    }
    return true;
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
  ngOnInit(): void {

  }
  public save(): void {

  }
}
