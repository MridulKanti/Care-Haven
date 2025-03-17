import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Orphanage } from 'src/app/models/orphanage.model';
import { OrphanageService } from 'src/app/services/orphanage.service';

@Component({
  selector: 'app-createorphanage',
  templateUrl: './createorphanage.component.html',
  styleUrls: ['./createorphanage.component.css']
})
export class CreateorphanageComponent implements OnInit {

  OrphanageForm: NgForm;
  orphanage: Orphanage = {
    OrphanageId: 0,
    OrphanageName: '',
    Description: '',
    Founder: '',
    EstablishmentDate: new Date(),
    Status: ''
  };

  checkTitle: string = "Orphanage Creation Form";

  orphanageId: number = 0;
  isRegister: boolean = true;
  
  constructor(private t1: OrphanageService, private router: Router, private Activateroute: ActivatedRoute) { }

  isFieldValid(fieldName: any): boolean {
    return !fieldName.valid && (fieldName.touched || fieldName.dirty);
  }

  ngOnInit(): void {
    this.Activateroute.paramMap.subscribe((data) => {
      this.orphanageId = +data.get('id');
      this.loadOrphanege();
    })
  }
  loadOrphanege() {
    
    if (this.orphanageId > 0) {
      this.checkTitle = "Edit Orphanage";
      this.isRegister = false;
      this.t1.getOrphanageById(this.orphanageId).subscribe((res) => {
        this.orphanage = res;
      });
    }
  }

  AddOrphanage(OrphanageForm: NgForm) {
    
    if (OrphanageForm.invalid) {
      console.log(OrphanageForm.controls.OrphanageName.errors.required);
    } else {
      this.t1.addOrphanage(this.orphanage).subscribe(() => {
        this.router.navigate([]);
      })
    }
  }

  editOrphanage() {
    this.t1.updateOrphanage(this.orphanageId, this.orphanage).subscribe(() => { });
  }
}  