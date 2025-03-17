import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { map } from 'rxjs/operators';
import {jwtDecode} from 'jwt-decode';


@Injectable({
  providedIn: 'root'
})
export class AuthguardGuard implements CanActivate {

  constructor(private service: AuthService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.service.isLoggedIn().pipe(
      map(isLoggedIn => {
        if (isLoggedIn) {
          const token = localStorage.getItem('token');
          const decoded = jwtDecode(token);
          const userRole = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
          if (route.data.roles && route.data.roles.indexOf(userRole) === -1) {
            console.log("User Role: "+userRole);
            console.log("Route Data Role: "+route.data.roles);
            this.router.navigate(['/error']);
            return false;
          }
          return true;
        }
        this.router.navigate(['/login']);
        return false;
      })
    );
  }
}
