import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { StoreModule } from '@ngrx/store';
import { AppComponent } from './app.component';
import { MaterialModule } from './shared/material.module';
import { EffectsModule } from '@ngrx/effects';
import { HttpClientModule } from '@angular/common/http';


const routes: Routes = [
  { path: 'benefits', loadChildren: () => import('./benefits/benefits.module').then(m => m.BenefitsModule) },
  { path: '**', redirectTo: 'benefits' }
];
@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    MaterialModule,
    BrowserModule,
    RouterModule.forRoot(routes),
    BrowserAnimationsModule,
    FormsModule,
    StoreModule.forRoot({}, {}),
    EffectsModule.forRoot([]),
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }





