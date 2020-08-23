import { DeletingVehiculeService } from './../../services/deleting-vehicule.service';
import { NotificationService } from './../../services/notification.service';
import { EditVehiculeService } from './../../services/edit-vehicule.service';
import { SaveVehicule } from './../../models/SaveVehicule';
import { Vehicule } from './../../models/Vehicule';

import { Component, OnInit, NgZone } from '@angular/core';
import { ActivatedRoute, Router, ActivationEnd } from '@angular/router';

import { MakeService } from 'src/app/services/make.service';
import { FeatureService } from 'src/app/services/feature.service';
import { AddingVehiculeService } from 'src/app/services/adding-vehicule.service';
import { UpdatingVehiculeService } from 'src/app/services/updating-vehicule.service';

import { forkJoin, Observable } from 'rxjs';

import * as _ from "underscore";

@Component({
  selector: 'app-adding-vehicule-form',
  templateUrl: './adding-vehicule-form.component.html',
  styleUrls: ['./adding-vehicule-form.component.css']
})
export class AddingVehiculeFormComponent implements OnInit {

  makes: any [] ;
  models: any [] ;
  vehicule: SaveVehicule = {
    id : 0,
    modelId : 0,
    makeId : 0,
    isRegistred : false,
    features : [],
    contact : {
      name : '',
      email : '',
      phone : ''
    }
  } ;
  features : any [];

  constructor(private makeService : MakeService , private featureService : FeatureService ,private route : ActivatedRoute , 
              private router : Router, private addingVehiculeService: AddingVehiculeService , private updatingVehiculeService :UpdatingVehiculeService , 
              private editVehiculeService : EditVehiculeService , private deletingVehiculeService : DeletingVehiculeService ,
              private notificationService: NotificationService) {
                
  }

  ngOnInit(): void {

    this.route.params.subscribe((params) => {
      this.vehicule.id = Number(params['id']);
    });   

    let sources : Observable<any>[] = [
      this.makeService.getAll(),
      this.featureService.getAll(),
    ]

    if (this.vehicule.id) {
      sources.push(this.editVehiculeService.get(this.vehicule.id));
    }

    forkJoin(sources).subscribe(data =>{
      this.makes = data[0] ;
      this.features = data[1];
      if (this.vehicule.id) {
        this.setVehicule(data[2])
        this.populateModels();
      }
      
    })
  }

  private setVehicule(v : Vehicule) {
    this.vehicule.id = v.id ;
    this.vehicule.makeId = v.make.id ;
    this.vehicule.modelId = v.model.id ;
    this.vehicule.isRegistred = v.isRegistred ;
    this.vehicule.features = _.pluck(v.features,'id');
    this.vehicule.contact = v.contact ;
  }

  private populateModels() {
    var selectedMake = this.makes.find(make => make.id == this.vehicule.makeId);
    this.models = selectedMake.models ;
  }

  onMakeChange(){
    this.populateModels()
    delete this.vehicule.modelId;
  }

  onFeatureToggle(feature , $event){
    if($event.target.checked){
      this.vehicule.features.push(feature.id)
    }
    else{
      var index = this.vehicule.features.indexOf(feature.id);
      this.vehicule.features.splice(index,1);
    }
  }

  submit (){
    if (this.vehicule.id) {
      this.updatingVehiculeService.update(this.vehicule).subscribe(
      x =>{
        this.notificationService.showSuccess("The vehicule was successfully updated","Success")
      })
    }
    else{
      this.addingVehiculeService.create(this.vehicule).subscribe(
        x => {
          console.log(x);
        }
      );
    }
  }

  delete(){
    if (confirm("Are you sure?")) {
      this.deletingVehiculeService.delete(this.vehicule.id).subscribe()
    }
  }

}
