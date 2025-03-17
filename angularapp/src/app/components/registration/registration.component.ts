import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  register: User = {
    Email: '',
    Password: '',
    Username: '',
    MobileNumber: '',
    UserRole: null
  };
  loader:boolean=false;
  confirmPassword: string = '';
  isVisible: boolean = false;
  isVisible2: boolean = false;
  emailExists:boolean=false;

  isFieldValid(fieldName: any): boolean {
    return !fieldName.valid && (fieldName.touched || fieldName.dirty);
  }

  registerForm() {
    this.loader = true;
    this.authService.register(this.register).subscribe(
      () => {
        this.loader = false;
        Swal.fire({
          title: 'Success!',
          text: 'Registration Successful!',
          icon: 'success',
          timer: 1500,
          showConfirmButton: false
        });
        this.router.navigate(['/login']);
      },
      (error) => {
        this.loader = false;
        if (error.status === 409) {
          console.error(error);
          this.emailExists=true;
        } else {
          this.loader = false;
          Swal.fire({
            title: 'Error!',
            text: 'Email Already Exists!!',
            icon: 'error',
            timer: 1500,
            showConfirmButton: false
          });
          console.error(error);
        }
      }
    );
  }  
  ngOnInit(): void {
  }


  togglePasswordVisibility(type:string)
  {
    if(type == 'password')
    {
        this.isVisible = !this.isVisible;
      const passwordField = document.getElementById('Password') as HTMLInputElement;
      passwordField.type = this.isVisible ? 'text' : 'password';
    }
    if(type == 'ConfirmPassword')
    {
      console.log(222);
      this.isVisible2 = !this.isVisible2;
      const passwordField = document.getElementById('confirmPassword') as HTMLInputElement;
      passwordField.type = this.isVisible2 ? 'text' : 'password';
    }
  }
}
