import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, switchMap } from 'rxjs';
import { environment } from '../../../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.baseUrl;
  public userDetails: any = {};

  constructor(private http: HttpClient) { }

  login(loginData: any): Observable<any> {
    return this.http.post(this.baseUrl + "User/Login", loginData);
  }

  logout() {
    localStorage.removeItem("accessToken");
  }

  register(loginData: any) {
    return this.http.post(this.baseUrl + "User/Register", loginData);
  }

  public set accesToken(token: any) {
    localStorage.setItem("accessToken", token)
    let decodedJWT = JSON.parse(window.atob(token.split('.')[1]));
    this.userDetails["name"] = decodedJWT.name;
    this.userDetails["role"] = decodedJWT.role;
  }

  public get isAuthenticated(): boolean {
    let token = localStorage.getItem("accessToken");
    if (token != null && token != undefined) {
      return true;
    }
    return false;
  }

  public get getToken() {
    return localStorage.getItem("accessToken");
  }
}
