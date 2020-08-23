import { AppErrorHandler } from './common/app-error-handler';
import { FeatureService } from './services/feature.service';
import { MakeService } from './services/make.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddingVehiculeFormComponent } from './components/adding-vehicule-form/adding-vehicule-form.component';
import { AddingVehiculeService } from './services/adding-vehicule.service';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { SentryErrorHandler } from './common/sentry-error-handler';
import { NavmenuComponent } from './components/navmenu/navmenu.component';
import { VehiculeListComponent } from './components/vehicule-list/vehicule-list.component';
import { PaginationComponent } from './components/shared/pagination/pagination.component';


@NgModule({
  declarations: [
    AppComponent,
    AddingVehiculeFormComponent,
    NavmenuComponent,
    VehiculeListComponent,
    PaginationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [
    {
      provide : ErrorHandler , useClass : AppErrorHandler
    },
    MakeService,
    FeatureService,
    AddingVehiculeService,
    SentryErrorHandler
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
