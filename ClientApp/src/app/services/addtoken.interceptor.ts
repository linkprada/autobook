import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable, throwError } from 'rxjs';
import { mergeMap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AddtokenInterceptor implements HttpInterceptor {

  constructor(private auth: AuthService) { }

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (this.auth.loggedIn) {
      return this.auth.getTokenSilently$().pipe(
        mergeMap(token => {
          const tokenReq = req.clone({
            setHeaders: { Authorization: `Bearer ${token}` }
          });
          return next.handle(tokenReq);
        }),
        catchError(err => throwError(err))
      );
    }
    else {
        return next.handle(req);
    }
  }
}