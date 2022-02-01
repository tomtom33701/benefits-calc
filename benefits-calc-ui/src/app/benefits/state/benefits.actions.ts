import { createAction, props } from "@ngrx/store";
import { IBenefitsCalculation } from '../Models/benefits-calculation';
import { IEmployeeDto } from './../Models/employee-dto';

export const loadEmployees = createAction(
  '[Employees] Load Employees'
);

export const saveEmployee = createAction(
  '[Employees] Save Employee',
  props<{employee: IEmployeeDto}>()
);

export const saveEmployeeSuccess = createAction(
  '[Employees] Save Employee Success',
  props<{employee: IEmployeeDto}>()
);

export const setCurrentEmployee = createAction(
  '[Employees] Set Current Employee',
  props<{employeeId: number | null}>()
);

export const loadEmployeesSuccess = createAction(
  '[Employees] Loaded Successfully',
  props<{employees: IEmployeeDto[]}>()
);

export const loadEmployeesFailure = createAction(
  '[Employees] Load Failed',
  props<{error: string}>()
);

export const requestBenefitQuote = createAction(
  '[Employees] Request Benefit Quote',
  props<{employee: IEmployeeDto}>()
);

export const requestBenefitQuoteSuccess = createAction(
  '[Employees] Request Benefit Quote Success',
  props<{calculation: IBenefitsCalculation}>()
);

export const requestBenefitQuoteFail = createAction(
  '[Employees] Request Benefit Quote Failed',
  props<{error: string}>()
);

export const saveEmployeeFailure = createAction(
  '[Employees] Save Failed',
  props<{error: string}>()
);
