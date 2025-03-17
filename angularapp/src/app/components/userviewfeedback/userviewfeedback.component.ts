import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Feedback } from 'src/app/models/feedback.model';
import { FeedbackService } from 'src/app/services/feedback.service';

@Component({
  selector: 'app-userviewfeedback',
  templateUrl: './userviewfeedback.component.html',
  styleUrls: ['./userviewfeedback.component.css']
})
export class UserviewfeedbackComponent implements OnInit {
  feedback: Feedback[]=[];
  selectedFeedbackId: number;
  loader :boolean =false;
  pageOfItems:Array<any>=[];
  currentIndex = -1;
  page = 1;
  count = 4;
  noRes=true;
  searchValue: string = '';

  constructor(private feedbackService: FeedbackService, private router: Router) { }

  ngOnInit(): void {
    this.loadFeedback();
  }

  loadFeedback(){
    this.loader=true;
    this.feedbackService.getFeedbacks().subscribe(data=>{
      this.feedback=data;
      this.loader=false;
    });
  }

  openModal(feedbackId : number)
  {
    this.selectedFeedbackId = feedbackId;
  }

  deleteConfirm(feedbackId:number):void{
    this.loader=true;
    this.feedbackService.deleteFeedback(feedbackId).subscribe(()=>{
    this.loader=false;
    this.loadFeedback();
      this.router.navigate(['/userviewfeedback']);
    })
  }
  searchData(value: string) {
    if (value === '') {
      this.loadFeedback();
    }
    else {
      this.feedback = this.feedback.filter(d =>
        d.FeedbackText.toLowerCase().includes(value.toLowerCase())
      );
    }
  }
}
