import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Orphanage } from 'src/app/models/orphanage.model';
import { DonationService } from 'src/app/services/donation.service';
import { OrphanageService } from 'src/app/services/orphanage.service';

@Component({
  selector: 'app-adminvieworphanage',
  templateUrl: './adminvieworphanage.component.html',
  styleUrls: ['./adminvieworphanage.component.css']
})
export class AdminvieworphanageComponent implements OnInit {

  orphan: Orphanage = {
    OrphanageId: 0,
    OrphanageName: '',
    Description: '',
    Founder: '',
    EstablishmentDate: new Date(),
    Status: ''
  }

  orphanage: Orphanage[];
  selectedOrphanageId: number;
  donationAmount: number = 0;
  pageOfItems: Array<any> = [];
  currentIndex = -1;
  page = 1;
  count = 4;
  tableView: boolean = true;
  cardView: boolean = false;
  loader:boolean = false;
  amount:number=0;

  constructor(private t1: OrphanageService, private t2: DonationService, private router: Router) { }

  ngOnInit(): void {
    this.loadOrphanage();
  }
  loadOrphanage(): void {
    this.loader = true;
    this.t1.getAllOrphanages().subscribe((res) => {
      this.loader = false;
      console.log(res);
      this.orphanage = res;
    });
  }

  
  openModal(orphanageId: number) {
    this.selectedOrphanageId = orphanageId;
    this.t2.getDonationsByOrphanageId(this.selectedOrphanageId).subscribe((res: any) => {
      res.forEach(item => {
        this.donationAmount += item.Amount;
      })
    });
  }

  resetDonation() {
    this.donationAmount = 0;
  }
  deleteConfirm(orphanageId: number): void {
    this.t1.deleteOrphanage(orphanageId).subscribe(() => {
      console.log("Orphanage Deleted");
      this.loadOrphanage();
      this.router.navigate(['/adminvieworphanage']);
    });
  }

  editConfirm(orphanageId: number): void {
    this.router.navigate(['/createorphanage', orphanageId]);
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
    filter = query.toUpperCase();
    let excludedColumn = 6;
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
}
