import { VehiculeService } from './../../services/vehicule.service';
import { NotificationService } from './../../services/notification.service';
import { SaveVehicule } from './../../models/SaveVehicule';
import { Vehicule } from './../../models/Vehicule';

import { Component, OnInit} from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';

import { MakeService } from 'src/app/services/make.service';
import { FeatureService } from 'src/app/services/feature.service';

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
              private router : Router, private notificationService: NotificationService , private vehiculeService : VehiculeService) {
                
  }

  ngOnInit(): void {

    this.route.params.subscribe(params => {
      this.vehicule.id = Number(params['id']) || 0;
    });   

    let sources : Observable<any>[] = [
      this.makeService.getAll(),
      this.featureService.getAll(),
    ]

    if (this.vehicule.id) {
      sources.push(this.vehiculeService.get(this.vehicule.id));
    }

    forkJoin(sources).subscribe(data =>{
      this.makes = data[0] ;
      this.features = data[1];
      if (this.vehicule.id) {
        this.setVehicule(data[2]);
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
    let result$ = (this.vehicule.id) ? this.vehiculeService.update(this.vehicule) : this.vehiculeService.create(this.vehicule) ;

    result$.subscribe((vehicule:Vehicule) =>{
      this.notificationService.showSuccess("Date saved successfully","Success"),
      this.router.navigate(['/vehicules/',vehicule.id]);
    });

    
  }

}
