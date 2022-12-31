import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  baseUrl = environment.baseUrl;
  private userDashboardSubject = new BehaviorSubject<any>([]);
  userDashboardData$ = this.userDashboardSubject.asObservable();

  constructor(private http: HttpClient) { }


  getUserDashBoard(){
   this.http.get(this.baseUrl + "Dashboard/UserDashboard").subscribe({
    next:(dashboardResponse:any)=>{
      this.userDashboardSubject.next(dashboardResponse);
    },
    error:(err:any)=>{

    }
   });
  }

  getAdminDashboard(){
    return this.http.get(this.baseUrl + "Dashboard/AdminDashboard")
  }

}
