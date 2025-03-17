import { Component, OnInit } from '@angular/core';
import { Donation } from 'src/app/models/donation.model';
import { DonationService } from 'src/app/services/donation.service';
import { NgxPaginationModule } from 'ngx-pagination';
@Component({
  selector: 'app-viewalldonation',
  templateUrl: './viewalldonation.component.html',
  styleUrls: ['./viewalldonation.component.css']
})
export class ViewalldonationComponent implements OnInit {

  donations: any[] = [];
  loader: boolean = false;
  pageOfItems:Array<any>=[];
  currentIndex = -1;
  page = 1;
  count = 10;
  tableView:boolean = true;
  cardView:boolean = false;

  constructor(private service: DonationService) { }

  ngOnInit(): void {
    this.loader = true;
    this.service.getAllDonations().subscribe((data) => {
      this.loader = false;
      this.donations = data;
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
    filter = query.toUpperCase();
    table = document.getElementsByClassName('table')[0];
    tr = table.getElementsByTagName('tr');

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName('td');
        for (let j = 0; j < td.length; j++) {
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
