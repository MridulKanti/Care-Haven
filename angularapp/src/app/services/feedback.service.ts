import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Feedback } from '../models/feedback.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {
  private apiUrl=environment.apiUrl;
  private headers= new HttpHeaders({'Content-Type': 'application/json', 'Authorization': 'Bearer' + localStorage.getItem('token')
  });
  
  constructor(private http: HttpClient) { }

  sendFeedback(feedback: Feedback): Observable<Feedback> {
    return this.http.post<Feedback>(this.apiUrl+"/api/feedback", feedback, {headers: this.headers});
  }

  getAllFeedbackByUserId(userId:number):Observable<Feedback[]>{
    return this.http.get<Feedback[]>(`${this.apiUrl}/api/feedback/${userId}`,{headers: this.headers});
  }

  deleteFeedback(feedbackId: number):Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/api/feedback/${feedbackId}`,{headers: this.headers});
  }

  getFeedbacks():Observable<Feedback[]>{
    return this.http.get<Feedback[]>(this.apiUrl+'/api/feedback',{headers: this.headers});
  }
}