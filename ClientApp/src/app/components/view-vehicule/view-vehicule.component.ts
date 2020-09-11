import { AuthService } from './../../services/auth.service';
import { ProgressService } from 'src/app/services/progress.service';
import { PhotoService } from './../../services/photo.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { VehiculeService } from 'src/app/services/vehicule.service';
import { HttpEvent, HttpResponse } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-view-vehicule',
  templateUrl: './view-vehicule.component.html',
  styleUrls: ['./view-vehicule.component.css']
})
export class ViewVehiculeComponent implements OnInit {
  @ViewChild('fileInput') fileInput : ElementRef ;
  vehicule: any ;
  vehiculeId ;
  photos : any[];
  progress : any ;

  constructor(private route : ActivatedRoute , private router : Router , private vehiculeService : VehiculeService, 
              private photoService : PhotoService , private progressService : ProgressService , public authService : AuthService) {

  }

  ngOnInit(): void {
    this.route.params.subscribe(params =>{
      this.vehiculeId = Number(params['id']);
      if (isNaN(this.vehiculeId) || this.vehiculeId <= 0) {
        this.router.navigate(['/vehicules']);
      }
    });

    this.vehiculeService.get(this.vehiculeId).subscribe(v =>{
      this.vehicule = v ;
    })

    this.photoService.get(this.vehiculeId,"photos").subscribe((p:any[]) => {
      this.photos = p ;
    })
  }

  delete(){
    if (confirm("Are you sure?")) {
      this.vehiculeService.delete(this.vehiculeId).subscribe()
    }
  }

  uploadPhoto () {
    var nativeElement : HTMLInputElement = this.fileInput.nativeElement ;
    var file = nativeElement.files[0] ;
    nativeElement.value = "";

    this.photoService.upload(this.vehiculeId,file,"photos")
    .subscribe(event => {
      // if (!(p instanceof HttpResponse)) {
      //   this.progress = this.progressService.getProgress(p,this.photos);
      // }
      // else{
      //   this.photos.push(p.body);
      // }
      this.progress = this.progressService.getProgress(event,this.photos);
    })
  }
  

}
