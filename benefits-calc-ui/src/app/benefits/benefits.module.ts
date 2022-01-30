import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { MaterialModule } from './../shared/material.module';
import { BenefitsComponent } from './benefits.component';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EmployeeComponent } from './components/employee/employee.component';
import { FooterComponent } from './components/footer/footer.component';
import { HomeComponent } from './components/home/home.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { employeeBenefitsReducer } from './state/benefits.reducer';
import { HttpClientModule } from '@angular/common/http';
import { BenefitsEffects } from './effects/benefits.effects';
import { BenefitPriceComponent } from './components/benefit-price/benefit-price.component';


const routes: Routes = [
  {
    path: '', component: BenefitsComponent,
    children: [
      { path: 'employee', component: EmployeeComponent },
      { path: 'employee-list', component: EmployeeListComponent },
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent }
    ]
  }
];

@NgModule({
  declarations: [
    BenefitsComponent,
    NavBarComponent,
    EmployeeListComponent,
    FooterComponent,
    HomeComponent,
    EmployeeComponent,
    BenefitPriceComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    StoreModule.forFeature('benefits', employeeBenefitsReducer),
    HttpClientModule,
    EffectsModule.forFeature([BenefitsEffects])
  ],
  exports: [RouterModule]
})
export class BenefitsModule { }
