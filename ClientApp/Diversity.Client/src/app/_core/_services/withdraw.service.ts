import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WithdrawService {

  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  createwithdrawRequest(withdrawData: any) {
    return this.http.post(this.baseUrl + "Withdraw/CreateWithdrawRequest", withdrawData);
  }

  getwithdrawRequestForUser(){
    return this.http.get(this.baseUrl + "Withdraw/GetUserWithdrawRequests");
  }

}
