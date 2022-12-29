import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DepositService {

  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  createDepositRequest(depositData: any) {
    return this.http.post(this.baseUrl + "DepositRequest/CreateDeposit", depositData);
  }

  getDepositRequestForUser(){
    return this.http.get(this.baseUrl + "DepositRequest/getUserDepositRequests");
  }
}
