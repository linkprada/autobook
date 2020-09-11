import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { HttpXhrBackend, HttpEventType, XhrFactory, HttpEvent } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProgressService {
  progress: Number;

  uploadProgress : Subject<any> = new Subject();
  downloadProgress : Subject<any> = new Subject();

  constructor() {
    
  }

  public getProgress(event : HttpEvent<Object>,fileStore : any[]) {
    switch (event.type) {
      case HttpEventType.Sent:
        console.log('Request has been made!');
        break;
      case HttpEventType.ResponseHeader:
        console.log('Response header has been received!');
        break;
      case HttpEventType.UploadProgress:
        console.log(`Uploaded! ${this.createProgress(event)}%`);
        return this.createProgress(event);
        break;
      case HttpEventType.DownloadProgress:
        console.log(`Download! ${this.createProgress(event)}%`);
        return this.createProgress(event);
        break;
      case HttpEventType.Response:
        console.log('successfully created!', event.body);
        fileStore.push(event.body)
        this.progress = 0;
        return event
        break;
    }
  }

  public createProgress(event) {
    return this.progress = Math.round(event.loaded / event.total * 100);
  }

}


// @Injectable({
//   providedIn: 'root'
// })
// export class ProgressXhrFactory extends XhrFactory{
 
//   constructor( private service : ProgressService) { super() }
  
//   build(): XMLHttpRequest {
//     let xhr : XMLHttpRequest

//     xhr.onprogress = (event => this.service.downloadProgress.next({
//       total : event.total,
//       percentage : Math.round(event.loaded / event.total * 100)
//     }));

//     xhr.upload.onprogress = (event => this.service.uploadProgress.next({
//       total : event.total,
//       percentage : Math.round(event.loaded / event.total * 100)
//     }));

//     return xhr
//   }
// }

