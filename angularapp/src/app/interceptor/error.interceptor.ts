import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    console.log('Request intercepted');
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error('Error occurred:', error);
        console.log('Error status:', error.status);
        if (error.status === 401) {
          this.router.navigate(['/401']);
        } else if (error.status === 500) {
          this.router.navigate(['/500']);
        }
        // else if (error.status === 409) {
        //   this.router.navigate(['/409']);
        // }
        else if (error.status === 200) { }
        // else {
        //   this.router.navigate(['/error']);
        // }
        return throwError(() => new Error(error.message));
      })
    );
  }
}
