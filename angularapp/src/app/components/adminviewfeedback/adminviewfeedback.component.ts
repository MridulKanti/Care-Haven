import { Component, OnInit } from '@angular/core';
import { FeedbackService } from 'src/app/services/feedback.service';
import { OrphanageService } from 'src/app/services/orphanage.service';
import { NgxPaginationModule } from 'ngx-pagination';

@Component({
  selector: 'app-adminviewfeedback',
  templateUrl: './adminviewfeedback.component.html',
  styleUrls: ['./adminviewfeedback.component.css']
})
export class AdminviewfeedbackComponent implements OnInit {

  constructor(private feedbackService: FeedbackService, private orphanageService: OrphanageService) { }
  feedback:any=[];
  user:any;
  loader:boolean=false;
  pageOfItems:Array<any>=[];
  currentIndex = -1;
  page = 1;
  count = 4;
  noRes:boolean=false;
  tableView:boolean = true;
  cardView:boolean = false;

  ngOnInit(): void {
    this.getFeedback();
  }

  getFeedback():void{
    this.loader = true;
    this.feedbackService.getFeedbacks().subscribe((data)=>{
      this.loader = false;
      this.feedback = data;
    })
  }

  openModal(userData?: any) : void{
    this.user = userData;
    this.loader=false;
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

    if(tr.length==0) {this.noRes=true;
      console.log(tr.length);}
    else {this.noRes=false;
    console.log(tr.length);}
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
                }
                 else {
                    tr[i].style.display = 'none';
                }
            }
            if (noResult) {
              document.getElementById('no-result').style.display = '';
              this.noRes=true;
            } else {
              document.getElementById('no-result').style.display = 'none';
              this.noRes=false;
            }
        }
    }
  }
  onChangePage(pageOfItems: Array<any>){
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
