import { BadInputError } from './../common/bad-input-error';
import { NotFoundError } from './../common/not-found-error';
import { AppError } from './../common/app-error';
import { HttpClient } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';



export class DataService {

  constructor(private http : HttpClient , private url : string) {

  }

  getAll(){
    return this.http.get<any[]>(this.url).pipe(catchError(this.handleError))
  }

  create(resource){
    return this.http.post(this.url,resource).pipe(catchError(this.handleError))
  }

  update(resource){
    return this.http.patch(this.url + "/" + resource.id, {isRead : true}).pipe(catchError(this.handleError))
  }

  delete(id){
    return this.http.delete(this.url + "/" + id).pipe(catchError(this.handleError))
  }
  
  private handleError(error : Response) {
    switch (error.status) {
      case 400:
        return throwError(new BadInputError())
        break;
      case 404:
        return throwError(new NotFoundError())
        break;
    
      default:
        return throwError(new AppError(error))
        break;
    }
  }

}
