import { VehiculeListComponent } from './components/vehicule-list/vehicule-list.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddingVehiculeFormComponent } from './components/adding-vehicule-form/adding-vehicule-form.component';


const routes: Routes = [
  { path: '', redirectTo: 'vehicules', pathMatch: 'full' },
  
  { path: 'vehicules/new', component : AddingVehiculeFormComponent },
  { path: 'vehicules/:id', component : AddingVehiculeFormComponent },
  { path: 'vehicules', component : VehiculeListComponent },

  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
