import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-adminnav',
  templateUrl: './adminnav.component.html',
  styleUrls: ['./adminnav.component.css']
})
export class AdminnavComponent implements OnInit {

  tempName:string;
  tempRole:string;
  constructor() { }

  ngOnInit(): void {
    this.tempName = localStorage.getItem('UserName');
    this.tempRole = localStorage.getItem('UserRole');
  }

  logOut(){
    localStorage.clear();
    console.log("Logged Out Successfully!");
  }

}
