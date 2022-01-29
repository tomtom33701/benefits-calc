import { IEmployee } from '@app/benefits/Models/employee';
import { createFeatureSelector, createReducer, createSelector, on } from "@ngrx/store";
import { IBenefitsRate } from '../Models/benefits-calculation';
import * as Actions from './benefits.actions';

export interface IBenefitsState {
  employees: IEmployee[];
  benefitsCalculation: IBenefitsRate[];
  currentEmployeeId: number | null;
  error: string;
}

const getEmployeesFeatureState = createFeatureSelector<IBenefitsState>('employees');

export const getEmployees = createSelector(
  getEmployeesFeatureState,
  state => state.employees
);

export const getError = createSelector(
  getEmployeesFeatureState,
  state => state.error
);

export const getCurrentEmployeeId = createSelector(
  getEmployeesFeatureState,
  state => state.currentEmployeeId
);

export const getCurrentEmployee = createSelector(
  getEmployeesFeatureState,
  getCurrentEmployeeId,
  (state, employeeId) => state.employees.find(e => e.employeeId === employeeId)
);

const initialState: Readonly<IBenefitsState> = {
  employees: [],
  benefitsCalculation: [],
  currentEmployeeId: null,
  error: ''
}
export const employeeBenefitsReducer = createReducer<IBenefitsState>(
  initialState,
  on(Actions.loadEmployees, state => {
    return {
      ...state
    };
  }),
  on(Actions.setCurrentEmployee, (state, {employeeId}) => {
    return {
      ...state,
      currentEmployeeId: employeeId
    };
  }),
  on(Actions.loadEmployeesSuccess, (state, {employees}) => {
    return {
      ...state,
      employees: employees,
      error: ''
    };
  }),
  on(Actions.saveEmployeesFailure, (state, {error}) => {
    return {
      ...state,
      error
    };
  }),
  on(Actions.loadEmployeesFailure, (state, {error}) => {
    return {
      ...state,
      employees: [],
      currentEmployeeId: null,
      error: error
    };
  }),
  on(Actions.saveEmployeeSuccess, (state, {employee}) => {
    const clone = state.employees.slice(0);
    clone.push(employee);
    return {
      ...state,
      employees: clone
    };
  })
);
