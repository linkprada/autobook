import { AddtokenInterceptor } from './services/addtoken.interceptor';
import { AdminComponent } from './components/admin/admin.component';
import { VehiculeListComponent } from './components/vehicule-list/vehicule-list.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddingVehiculeFormComponent } from './components/adding-vehicule-form/adding-vehicule-form.component';
import { ViewVehiculeComponent } from './components/view-vehicule/view-vehicule.component';
import { AuthGuard } from './services/auth.guard';
import { AdminAuthGuard } from './services/admin-auth.guard';
import { HTTP_INTERCEPTORS } from '@angular/common/http';


const routes: Routes = [
  { path: '', redirectTo: 'vehicules', pathMatch: 'full' },
  
  { path: 'vehicules/new', component : AddingVehiculeFormComponent , canActivate: [AuthGuard]},
  { path: 'vehicules/admin', component : AdminComponent , canActivate: [AdminAuthGuard] , data: { roles: ['Admin'] } },
  { path: 'vehicules/:id', component : ViewVehiculeComponent },
  { path: 'vehicules/edit/:id', component : AddingVehiculeFormComponent , canActivate: [AuthGuard]},
  { path: 'vehicules', component : VehiculeListComponent },

  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AddtokenInterceptor,
      multi: true
    }
  ]
})
export class AppRoutingModule { }
