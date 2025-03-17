import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Orphanage } from '../models/orphanage.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrphanageService {


public apiUrl:string=environment.apiUrl;

private headers=new HttpHeaders({'Content-Type': 'application/json','Authorization': 'Bearer'+localStorage.getItem('token')});


constructor(private http:HttpClient) { }

getAllOrphanages():Observable<Orphanage[]>
{
return this.http.get<Orphanage[]>(this.apiUrl+"/api/orphanages",{headers:this.headers});
}

getOrphanageById(orphanageId:number):Observable<Orphanage>
{
return this.http.get<Orphanage>(`${this.apiUrl}/api/orphanages/${orphanageId}`,{headers:this.headers});


}

addOrphanage(orphanage:Orphanage):Observable<any>
{
return this.http.post<any>(this.apiUrl+"/api/orphanages",orphanage,{headers:this.headers});

}

updateOrphanage(orphanageId:number,orphanage:Orphanage):Observable<any>
{
return this.http.put<any>(`${this.apiUrl}/api/orphanages/${orphanageId}`,orphanage,{headers:this.headers})


}

deleteOrphanage(orphanageId:number):Observable<any>
{
return this.http.delete<any>(`${this.apiUrl}/api/orphanages/${orphanageId}`,{headers:this.headers});


}







}
