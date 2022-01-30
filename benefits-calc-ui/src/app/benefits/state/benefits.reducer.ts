import { IEmployee } from '@app/benefits/Models/employee';
import { createFeatureSelector, createReducer, createSelector, on } from "@ngrx/store";
import { IBenefitsCalculation } from '../Models/benefits-calculation';
import * as Actions from './benefits.actions';

export interface IBenefitsState {
  employees: IEmployee[];
  benefitsCalculation: IBenefitsCalculation | null;
  currentEmployeeId: number | null;
  error: string;
}
const initialState: Readonly<IBenefitsState> = {
  employees: [],
  benefitsCalculation: null,
  currentEmployeeId: null,
  error: ''
};

const getEmployeesFeatureState = createFeatureSelector<IBenefitsState>('benefits');

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

export const getBenefitQuote = createSelector(
  getEmployeesFeatureState,
  state => state.benefitsCalculation
);


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
  on(Actions.saveEmployeeFailure, (state, {error}) => {
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
      error
    };
  }),
  on(Actions.saveEmployeeSuccess, (state, {employee}) => {
    const clone = state.employees.slice(0);
    clone.push(employee);
    return {
      ...state,
      employees: clone
    };
  }),
  on(Actions.requestBenefitQuote, (state) => {
    return {
      ...state,
      benefitsCalculation: null
    };
  }),
  on(Actions.requestBenefitQuoteSuccess, (state, {calculation}) => {
    return {
      ...state,
      benefitsCalculation: calculation,
      error: ''
    };
  }),
  on(Actions.requestBenefitQuoteFail, (state, {error}) => {
    return {
      ...state,
      benefitsCalculation: null,
      error
    };
  })
);
