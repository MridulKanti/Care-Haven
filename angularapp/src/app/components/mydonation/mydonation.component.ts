import { Component, OnInit } from '@angular/core';
import { Donation } from 'src/app/models/donation.model';
import { DonationService } from 'src/app/services/donation.service';
import { OrphanageService } from 'src/app/services/orphanage.service';
@Component({
  selector: 'app-mydonation',
  templateUrl: './mydonation.component.html',
  styleUrls: ['./mydonation.component.css']
})
export class MydonationComponent implements OnInit {
  
  donations:any[]=[];
  orphanage:any;
  loader:boolean;
  tableView:boolean = true;
  cardView:boolean = false;
  constructor(private DonationService:DonationService, private orphanageService: OrphanageService) { }

  ngOnInit(): void {
     this.getDonation();
  }
   
  getDonation()
  {
    this.loader = true;
    let uId = Number(localStorage.getItem('UserId'));
    this.DonationService.getDonationsByUserId(uId).subscribe((data)=>{
      this.donations=data;
      this.loader = false;
    });

  }
  openModal(orphanageId?: number) : void
  {
    this.loader = true;
    this.orphanageService.getOrphanageById(orphanageId).subscribe(data=>{
      this.orphanage = data;
      this.loader=false;
    })
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
    let excludedColumn = 4;
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
