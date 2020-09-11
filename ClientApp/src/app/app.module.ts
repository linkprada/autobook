import { PhotoService } from './services/photo.service';
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

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { SentryErrorHandler } from './common/sentry-error-handler';
import { NavmenuComponent } from './components/navmenu/navmenu.component';
import { VehiculeListComponent } from './components/vehicule-list/vehicule-list.component';
import { PaginationComponent } from './components/shared/pagination/pagination.component';
import { ViewVehiculeComponent } from './components/view-vehicule/view-vehicule.component';
import { VehiculeService } from './services/vehicule.service';
import { ProgressService } from './services/progress.service';
import { AdminComponent } from './components/admin/admin.component';
import { AuthService } from './services/auth.service';
import { AuthGuard } from './services/auth.guard';
import { NgxChartsModule }from '@swimlane/ngx-charts';


@NgModule({
  declarations: [
    AppComponent,
    AddingVehiculeFormComponent,
    NavmenuComponent,
    VehiculeListComponent,
    PaginationComponent,
    ViewVehiculeComponent,
    AdminComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    NgxChartsModule
  ],
  providers: [
    //{ provide : ErrorHandler , useClass : AppErrorHandler },
    MakeService,
    FeatureService,
    VehiculeService,
    PhotoService,
    SentryErrorHandler,
    ProgressService,
    AuthService,
    AuthGuard,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
