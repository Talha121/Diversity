import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getAllUsers() {
    return this.http.get(this.baseUrl + "User/GetAllUsers");
  }

  getUserProfile() {
    return this.http.get(this.baseUrl + "User/GetUserProfile");
  }

  updateUserProfile(data:any) {
    return this.http.post(this.baseUrl + "User/UpdateProfile",data);
  }

  getUserKyc() {
    return this.http.get(this.baseUrl + "UserKYC/GetKYCByUser");
  }

  getAllUserKyc() {
    return this.http.get(this.baseUrl + "UserKYC/GetAllKYC");
  }

  createKyc(data:any) {
    return this.http.post(this.baseUrl + "UserKYC/CreateKYC",data);
  }

  updateKycStatus(data:any) {
    return this.http.post(this.baseUrl + "UserKYC/updatekycstatus",data);
  }
}
