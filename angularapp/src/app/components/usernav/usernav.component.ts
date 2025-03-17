import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-usernav',
  templateUrl: './usernav.component.html',
  styleUrls: ['./usernav.component.css']
})
export class UsernavComponent implements OnInit {
  tempName : string
  tempRole: string;
  constructor(private router:Router) { }

  ngOnInit(): void {
    this.tempName = localStorage.getItem('UserName');
    this.tempRole = localStorage.getItem('UserRole');
  }

  logOut(){
    localStorage.clear();
    this.router.navigate(['/login']);
    console.log("Logged Out Successfully!");
  }

}
