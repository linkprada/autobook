import { FileService } from './file.service';
import { DataService } from './data.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PhotoService extends DataService {

  constructor(http : HttpClient) { 
    //super(http , `/Vehicules/${vehiculeId}/photos/`);
    super(http , `/Vehicules`);
  }
  
}
