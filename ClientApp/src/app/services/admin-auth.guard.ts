import { AuthService } from './auth.service';
import { AuthGuard } from './auth.guard';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminAuthGuard extends AuthGuard {

  constructor(private authService : AuthService , private router : Router){
    super(authService)
  }

  canActivate(next: ActivatedRouteSnapshot,state: RouterStateSnapshot) {
    let isAuthenticated = super.canActivate(next,state);

    let roles : string[] = next.data["roles"];

    let isAuthorized = isAuthenticated ? this.authService.isInRole(roles[0]) : false

    if (!isAuthorized) {
      this.router.navigate(['/vehicules']);
    }
    
    return isAuthorized;
  }
  
}
