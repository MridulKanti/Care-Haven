import { Component, OnInit } from '@angular/core';
import { Feedback } from 'src/app/models/feedback.model';
import { FeedbackService } from 'src/app/services/feedback.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-useraddfeedback',
  templateUrl: './useraddfeedback.component.html',
  styleUrls: ['./useraddfeedback.component.css']
})
export class UseraddfeedbackComponent implements OnInit {
  
  newFeedback: Feedback={
    FeedbackId: 0,
    FeedbackText: '',
    UserId: Number(localStorage.getItem('UserId')),
    Date: undefined
  };
  
  formSubmitted: boolean=false;

  constructor(private feedbackservice: FeedbackService, private router: Router) { }


  ngOnInit(): void {
    this.newFeedback.Date = new Date();
  }

  addFeedback():void{
    if (this.isFormValid()){
      this.formSubmitted=true;
      this.feedbackservice.sendFeedback(this.newFeedback).subscribe(()=>{
      })
    }
  }
  
  successForm(): void
  {
    this.router.navigate(['/userviewfeedback']);
    this.feedbackservice.getAllFeedbackByUserId(this.newFeedback.UserId).subscribe(()=>{
    })
  }
  isFieldInvalid(fieldname:string):boolean {
    return !this.newFeedback[fieldname];
  }

  isFormValid(): boolean{
    const data=['FeedbackText'];
    for (let x of data){
      if(this.isFieldInvalid(x)){
        return false;
      }
    }
    return true;
  }

}
