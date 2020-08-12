import { FeatureService } from './services/feature.service';
import { MakeService } from './services/make.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddingVehiculeFormComponent } from './adding-vehicule-form/adding-vehicule-form.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    AddingVehiculeFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'vehicule/new', component : AddingVehiculeFormComponent },

      { path: '**', redirectTo: 'home' }
  ])
  ],
  providers: [
    MakeService,
    FeatureService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
