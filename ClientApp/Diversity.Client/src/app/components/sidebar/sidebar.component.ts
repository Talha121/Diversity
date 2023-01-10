import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/_core/_services/auth.service';
import { environment } from 'src/environments/environment';

declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}
export const UserROUTES: RouteInfo[] = [
    { path: '/user/dashboard', title: 'Dashboard',  icon: 'ni-tv-2 text-primary', class: '' },
    { path: '/user/deposit', title: 'Deposit',  icon:'ni-money-coins text-info', class: '' },
    { path: '/user/grab-order', title: 'Grab Order',  icon:'ni-cart text-orange', class: '' },
    { path: '/user/order', title: 'Orders',  icon:'ni-bag-17 text-blue', class: '' },
    { path: '/user/withdraw', title: 'Withdraw',  icon:'ni-money-coins text-green', class: '' },
    { path: '/user/profile', title: 'Profile',  icon:'ni-single-02 text-yellow', class: '' },
];
export const AdminROUTES: RouteInfo[] = [
  { path: '/admin/admin-dashboard', title: 'Dashboard',  icon: 'ni-tv-2 text-primary', class: '' },
  { path: '/admin/user', title: 'Users',  icon:'ni-single-02 text-yellow', class: '' },
  { path: '/admin/admin-orders', title: 'Order',  icon:'ni-bag-17 text-blue', class: '' },
  { path: '/admin/admin-deposit', title: 'Deposit',  icon:'ni-money-coins text-info', class: '' },
  { path: '/admin/admin-withdraw', title: 'Withdraw',  icon:'ni-money-coins text-green', class: '' },
  { path: '/admin/product', title: 'Products',  icon:'ni-cart text-orange', class: '' },
  { path: '/admin/userKyc', title: 'Users Kyc',  icon:'ni-single-02 text-green', class: '' },
  // { path: '/icons', title: 'Icons',  icon:'ni-planet text-blue', class: '' },
  // { path: '/maps', title: 'Maps',  icon:'ni-pin-3 text-orange', class: '' },
  // { path: '/user-profile', title: 'User profile',  icon:'ni-single-02 text-yellow', class: '' },
  // { path: '/tables', title: 'Tables',  icon:'ni-bullet-list-67 text-red', class: '' },
  // { path: '/login', title: 'Login',  icon:'ni-key-25 text-info', class: '' },
  // { path: '/register', title: 'Register',  icon:'ni-circle-08 text-pink', class: '' }
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  public menuItems: any[];
  public isCollapsed = true;
  userData:any={};
  constructor(private router: Router,private authService:AuthService) { }

  ngOnInit() {
    let data=this.authService.DecodedToken;
    if(data.role=='User'){
      this.menuItems = UserROUTES.filter(menuItem => menuItem);
      this.router.events.subscribe((event) => {
        this.isCollapsed = true;
     });
    }
    if(data.role=='Admin'){
      this.menuItems = AdminROUTES.filter(menuItem => menuItem);
      this.router.events.subscribe((event) => {
        this.isCollapsed = true;
     });
    }
    this.getDataFromToken();
  }
  getDataFromToken(){
    this.userData=this.authService.DecodedToken;
  }
  logoutUser(){
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
