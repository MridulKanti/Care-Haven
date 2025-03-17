import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/login.model';
import { AuthService } from 'src/app/services/auth.service';
import { jwtDecode } from 'jwt-decode';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginUser: Login ={
    Email: '',
    Password: ''
  }
  loader:boolean;
  isVisible: boolean = false;

  constructor(private service: AuthService, private router: Router) { }

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if(token) this.router.navigate(['/home']);
  }

  login(){
    var tempToken = localStorage.getItem('token');
    if(tempToken) localStorage.clear();
    this.loader = true;
    this.service.login(this.loginUser).subscribe((response)=>{
    this.loader = false;
    localStorage.setItem('token',response.token);
    localStorage.setItem('UserId',this.getUserId());
    localStorage.setItem('UserName',this.getUserName());
    localStorage.setItem('UserEmail',this.getUserEmail());
    localStorage.setItem('UserRole',this.getUserRole());
    this.router.navigate(['/home']);
    Swal.fire({
      title: 'Success!',
      text: 'Login Successful!',
      icon: 'success',
      timer: 1500,
      showConfirmButton: false
    });
  });
  }

  isFieldValid(fieldName: any): boolean {
    return !fieldName.valid && (fieldName.touched || fieldName.dirty);
  }

  getUserId(){
    var tempToken = localStorage.getItem('token');
    var decoded = jwtDecode(tempToken);
    var id = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'][0];
    return id;
  }
  
  getUserName(){
    var tempToken = localStorage.getItem('token');
    var decoded = jwtDecode(tempToken);
    var name = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'][1];
    return name;
  }
  
  getUserEmail(){
    var tempToken = localStorage.getItem('token');
    var decoded = jwtDecode(tempToken);
    var email = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'];
    return email;
  }
  
  getUserRole(){
    var tempToken = localStorage.getItem('token');
    var decoded = jwtDecode(tempToken);
    var role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    return role;
  }
  togglePasswordVisibility(): void {
    this.isVisible = !this.isVisible;
    const passwordField = document.getElementById('Password') as HTMLInputElement;
    passwordField.type = this.isVisible ? 'text' : 'password';
  }
}