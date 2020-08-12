import { FeatureService } from './../services/feature.service';
import { MakeService } from '../services/make.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-adding-vehicule-form',
  templateUrl: './adding-vehicule-form.component.html',
  styleUrls: ['./adding-vehicule-form.component.css']
})
export class AddingVehiculeFormComponent implements OnInit {

  makes: any [] ;
  models: any [] ;
  vehicule: any = {} ;
  isRegistred =["yes","no"]
  features : any [];

  constructor(private makeService : MakeService , private featureService : FeatureService) {

  }

  ngOnInit(): void {
    this.makeService.getAll().subscribe(
      makes => {
        this.makes = makes ;
      }
    );
    
    this.featureService.getAll().subscribe(
      features => {
        this.features = features;
      }
    )
  }

  onMakeChange(){
    var selectedMake = this.makes.find(make => make.id == this.vehicule.make);
    this.models = selectedMake.models ;
  }

}
