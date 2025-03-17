import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Donation } from 'src/app/models/donation.model';
import { Orphanage } from 'src/app/models/orphanage.model';
import { DonationService } from 'src/app/services/donation.service';
import { OrphanageService } from 'src/app/services/orphanage.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-uservieworphanage',
  templateUrl: './uservieworphanage.component.html',
  styleUrls: ['./uservieworphanage.component.css']
})
export class UservieworphanageComponent implements OnInit {
  orphanage:Orphanage[];
  loader: boolean = false;
  tableView:boolean = true;
  cardView:boolean = false;
  pageOfItems:Array<any>=[];
  currentIndex = -1;
  page = 1;
  count = 4;
  isAmountValid:boolean = false;

  selectedOrphanageId:number;
  donation:Donation={
    DonationId:0,
    UserId: 0,
    OrphanageId: 0,
    Amount: null,
    DonationDate: new Date()
  }
  constructor(private t1: OrphanageService, private router: Router, private t2: DonationService) { }

  ngOnInit(): void {
    this.loadOrphanage();
  }

  loadOrphanage(): void {
    this.loader = true;
    this.t1.getAllOrphanages().subscribe((res) => {
      this.orphanage = res;
      this.loader = false;
    });

  }

  openModal(orphanageId: number) {
    this.selectedOrphanageId = orphanageId;
  }

  DonateConfirm(orphanageId: number): void {
    this.loader = true;
    var userId = +localStorage.getItem("UserId");
    this.donation.UserId = userId;
    this.donation.OrphanageId = orphanageId;
    this.t2.addDonation(this.donation).subscribe(() => {
      this.loader = false;
      Swal.fire({
        title: 'Thank You!',
        text: 'We are donating a portion of our proceeds to an orphanage.',
        icon: 'success',
        timer: 2000,
        showConfirmButton: false
      });
      this.router.navigate(["/mydonation"]);
    });

  }  

  onKeyUp(event: any): void {
    let query = event.target.value;
  if (query) {
    this.filterSearch(query);
  } else {
    document.getElementById('no-result').style.display = 'none';
    let table, tr;
    table = document.getElementsByClassName('table')[0];
    tr = table.getElementsByTagName('tr');
    for (let i = 0; i < tr.length; i++) {
      tr[i].style.display = '';
    }
  }
  }

  filterSearch(query: string): void {
    let filter, table, tr, td, i, txtValue;
    let noResult = true;
    let excludedColumn = 6;
    filter = query.toUpperCase();
    table = document.getElementsByClassName('table')[0];
    tr = table.getElementsByTagName('tr');

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName('td');
        for (let j = 0; j < td.length; j++) {
          if(j==excludedColumn){continue;}
            if (td[j]) {
                txtValue = td[j].textContent || td[j].innerText;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    tr[i].style.display = '';
                    noResult = false;
                    break;
                } else {
                    tr[i].style.display = 'none';
                }
            }
            if (noResult) {
              document.getElementById('no-result').style.display = '';
            } else {
              document.getElementById('no-result').style.display = 'none';
            }
        }
    }

  }
  onChangePage(pageOfItems: Array<any>) {
    this.pageOfItems = pageOfItems;
  }
  toggleView(viewType)
  {
    if(viewType == 'table')
    {
      this.tableView = true;
      this.cardView = false;
    }
    if(viewType == 'card')
    {
      this.tableView = false;
      this.cardView = true;
    }
  }
  validateAmount():void
  {
    if(this.donation.Amount==null || this.donation.Amount>=1)
    {
      this.isAmountValid = false;
    }
    if(this.donation.Amount<1)
    {
      this.isAmountValid = true;
    }
  }
}
