import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../models/user.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { Login } from '../models/login.model';
import { tap } from 'rxjs/operators';
import {jwtDecode} from 'jwt-decode';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}`;
  private loggedIn = new BehaviorSubject<boolean>(false);
  private userRole = new BehaviorSubject<string>('');

  constructor(private http: HttpClient) {
    const token = localStorage.getItem('token');
    if (token) {
      console.log("Logged In Success AuthService file");
      this.loggedIn.next(true);
      const decoded = jwtDecode(token);
      const role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      console.log("User Role AuthService file"+role);
      this.userRole.next(role);
    }
  }

  register(user: User): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/register`, user);
  }

  login(login: Login): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/login`, login).pipe(
      tap(response => {
        if (response && response.Token) {
          const token = response.Token;
          localStorage.setItem('token', token);
          const decoded = jwtDecode(token);
          const role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
          localStorage.setItem('userRole', role);
          this.loggedIn.next(true);
          this.userRole.next(role);
          console.log("AuthService UserRole: "+this.userRole);
        }
      })
    );
  }

  getUserRole(){
    var tempToken = localStorage.getItem('token');
    var decoded = jwtDecode(tempToken);
    var role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    return role;
  }

  isLoggedIn(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }

  isAdmin(): Observable<boolean> {
    var tempToken = localStorage.getItem('token');
    var decoded = jwtDecode(tempToken);
    var role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    return role.asObservable().pipe(
      map(role => role === 'Admin')
    );
  }

  isUser(): Observable<boolean> {
    var tempToken = localStorage.getItem('token');
    var decoded = jwtDecode(tempToken);
    var role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    return role.asObservable().pipe(
      map(role => role === 'User')
    );
  }

  getRole(): string {
    return this.userRole.value;
  }

  logout() {
    localStorage.clear();
    this.loggedIn.next(false);
    this.userRole.next('');
  }
}
