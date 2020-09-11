import { AuthService } from './../../services/auth.service';
import { MakeService } from './../../services/make.service';
import { KeyValuePair } from './../../models/KeyValuePair';
import { Vehicule } from './../../models/Vehicule';
import { Component, OnInit } from '@angular/core';
import { VehiculeService } from 'src/app/services/vehicule.service';

@Component({
  selector: 'app-vehicule-list',
  templateUrl: './vehicule-list.component.html',
  styleUrls: ['./vehicule-list.component.css']
})
export class VehiculeListComponent implements OnInit {
  private readonly PAGE_SIZE = 3 ;

  queryResult : any = {
    totalItems : 1 ,
    items : [] 
  };
  allVehicules : Vehicule[];
  makes : KeyValuePair[];
  query : any = {
    page : 1 ,
    pageSize : this.PAGE_SIZE ,
  };
  columns = [
    {title:"Id" },
    {title:"Make" , key:"make" , isSortable:true},
    {title:"Model" , key:"model" , isSortable:true},
    {title:"Contact Name" , key:"contactName" , isSortable:true},
    {/*the column "view"*/}
  ]

  constructor(private vehiculeService : VehiculeService , private makeService : MakeService ,public authService : AuthService) {

  }

  ngOnInit(): void {
    this.makeService.getAll().subscribe(makes => this.makes = makes);
    this.populateVehicules();
  }

  populateVehicules(){
    this.vehiculeService.getAll(this.query).subscribe(vehicules => this.queryResult = this.allVehicules = vehicules)
  }

  //filtering on server side
  onFilterChange() {
    this.query.page = 1 ;
    this.populateVehicules();  
  }

  resetFilter() {
    this.query = {
      page : 1 ,
      pageSize : this.PAGE_SIZE ,
    } ;
    this.populateVehicules() ;
  }

  //filtering on client side
  // onFilterChange() {
  //   let vehicules = this.allVehicules ;
  //   if (this.filter.makeId) {
  //     vehicules = vehicules.filter(v => v.make.id == this.filter.makeId) ;
  //   }
  //   if (this.filter.modelId) {
  //     vehicules = vehicules.filter(v => v.model.id == this.filter.modelId) ;
  //   }

  //   this.vehicules = vehicules ;
  // }

  sortBy(columnName){
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending ;
    } 
    else {
      this.query.sortBy = columnName ;
        this.query.isSortAscending = true ;
    }
    this.populateVehicules();
  }

  onPageChange(page){
    this.query.page = page ;
    this.populateVehicules();
  }
  
}
