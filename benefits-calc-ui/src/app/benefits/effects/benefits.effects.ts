import { Injectable } from "@angular/core";
import { BenefitsService } from "@app/services/benefits.services";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, concatMap, map, mergeMap, of } from 'rxjs';
import * as BenefitsActions from './../state/benefits.actions';

@Injectable()
export class BenefitsEffects {
  constructor(
    private actions$: Actions,
    private benefitsService: BenefitsService
    ) {}

    public loadEmployees$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(BenefitsActions.loadEmployees),
        mergeMap(() => this.benefitsService.getAllEmployees().pipe(
          map(employees => BenefitsActions.loadEmployeesSuccess({employees})),
          catchError(error => of(BenefitsActions.loadEmployeesFailure({error})))
        ))
      )
    });

    public saveEmployee$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(BenefitsActions.saveEmployee),
        concatMap(({employee}) => this.benefitsService.saveEmployee(employee).pipe(
          map(employee => BenefitsActions.saveEmployeeSuccess({employee})),
          catchError(error => of(BenefitsActions.saveEmployeesFailure(error)))
        ))
      )
    });
}
