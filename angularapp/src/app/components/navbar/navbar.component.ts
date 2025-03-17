import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private authService:AuthService) { }

  ngOnInit(): void {
    this.isLogin();
    this.isAdmin();
    this.isUser();
  }

  isLogin(){
    var token = localStorage.getItem('token'); 
    if(!token) return false;
    return true;
  }

  isAdmin(){
    let roleFetch = this.getRole();
    if(roleFetch === 'Admin') return true;
    return false;
  }
  
  isUser(){
    let roleFetch = this.getRole();
    if(roleFetch === 'User') return true;
    return false;
  }

  getRole():string{
    var token = localStorage.getItem('token'); 
    if(!token){
      return null;
    }
    var decoded = jwtDecode(token);
    let RoleType = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    return RoleType;
  }
}
