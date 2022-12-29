import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getOrderForUser(){
    return this.http.get(this.baseUrl + "Order/GetOrdersByUserId");
  }

  getCurrectOrder(){
    return this.http.get(this.baseUrl + "Order/GetUserCurrentOrder");
  }

  completeOrder(orderId:any){
    return this.http.get(this.baseUrl + "Order/CompleteOrder?orderId="+orderId);
  }

}
