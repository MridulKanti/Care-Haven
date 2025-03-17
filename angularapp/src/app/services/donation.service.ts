import { Injectable } from '@angular/core';
import { Donation } from '../models/donation.model';
import { Observable } from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class DonationService {

  private apiUrl=`${environment.apiUrl}`;
  private headers= new HttpHeaders({'Content-Type': 'application/json', 'Authorization': 'Bearer' + localStorage.getItem('token') }); 

  constructor(private http:HttpClient) { }

  getAllDonations():Observable<Donation[]>
  {
    return this.http.get<Donation[]>(this.apiUrl+"/api/donations",{headers: this.headers});
  }

  addDonation(donation:Donation):Observable<Donation>
  {
    return this.http.post<Donation>(this.apiUrl+"/api/donations",donation,{headers: this.headers});
  }

  getDonationsByUserId(userId:number):Observable<Donation[]>
  {
    return this.http.get<Donation[]>(`${this.apiUrl}/api/donations/User/${userId}`,{headers: this.headers});
  }

  getDonationsByOrphanageId(orphanageId:number):Observable<Donation[]>
  {
    return this.http.get<Donation[]>(`${this.apiUrl}/api/donations/orphanages/${orphanageId}`,{headers: this.headers});
  }
}

