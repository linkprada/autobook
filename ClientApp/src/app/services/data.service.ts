import { ProgressService } from 'src/app/services/progress.service';
import { BadInputError } from './../common/bad-input-error';
import { NotFoundError } from './../common/not-found-error';
import { AppError } from './../common/app-error';
import { HttpClient } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError , map} from 'rxjs/operators';



export class DataService {

  constructor(private http : HttpClient , private url : string ) {

  }

  private handleError(error : Response) {
    switch (error.status) {
      case 400:
        return throwError(new BadInputError());
        break;
      case 404:
        return throwError(new NotFoundError());
        break;
      default:
        return throwError(new AppError(error));
        break;
    }
  }

  getAll(filters?){
    return this.http.get<any[]>(this.url + "?" + this.toQueryString(filters)).pipe(catchError(this.handleError));
  }

  get(id,resourceName?){
    if (resourceName === undefined) {
      return this.http.get(this.url + "/" + id).pipe(catchError(this.handleError));
    }
    return this.http.get(this.url + "/" + id + "/" + resourceName).pipe(catchError(this.handleError));
  }

  create(resource){
    return this.http.post(this.url,resource).pipe().pipe(catchError(this.handleError));
  }

  update(resource){
    return this.http.patch(this.url + "/" + resource.id, resource).pipe(catchError(this.handleError));
  }

  delete(id){
    return this.http.delete(this.url + "/" + id).pipe(catchError(this.handleError));
  }

  upload(id , resource, resourceName){
    var formData = new FormData();
    formData.append("file",resource);
    return this.http.post(`${this.url}/${id}/${resourceName}` , formData , 
    {
      reportProgress: true,
      observe: 'events'
    }).pipe(catchError(this.handleError));
  }

  // extract to a class and refactor if needed
  toQueryString(obj){
    let parts = [] ;
    for(let property in obj){
      let value = obj[property] ;
      if (value != null && value != undefined) {
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
      }
    }
    return parts.join('&');
  }

}
