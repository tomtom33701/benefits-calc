import { IEmployee } from './../Models/employee';
import { createAction, props } from "@ngrx/store";

export const loadEmployees = createAction(
  '[Employees] Load Employees'
);

export const saveEmployee = createAction(
  '[Employees] Save Employee',
  props<{employee: IEmployee}>()
);

export const saveEmployeeSuccess = createAction(
  '[Employees] Save Employee Success',
  props<{employee: IEmployee}>()
);

export const setCurrentEmployee = createAction(
  '[Employees] Set Current Employee',
  props<{employeeId: number | null}>()
);

export const loadEmployeesSuccess = createAction(
  '[Employees] Loaded Successfully',
  props<{employees: IEmployee[]}>()
);

export const loadEmployeesFailure = createAction(
  '[Employees] Load Failed',
  props<{error: string}>()
);

export const saveEmployeesFailure = createAction(
  '[Employees] Save Failed',
  props<{error: string}>()
);
