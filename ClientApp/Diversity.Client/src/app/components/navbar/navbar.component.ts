import { Component, OnInit, ElementRef } from '@angular/core';
import { AdminROUTES, UserROUTES } from '../sidebar/sidebar.component';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/_core/_services/auth.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  public focus;
  public listTitles: any[];
  public location: Location;
  userData:any={};
  constructor(location: Location,  private element: ElementRef, private router: Router,private authService:AuthService) {
    this.location = location;
  }

  ngOnInit() {
    let data=this.authService.DecodedToken;
    if(data.role=='User'){
      this.listTitles = UserROUTES.filter(listTitle => listTitle);
    }
    if(data.role=='Admin'){
      this.listTitles = AdminROUTES.filter(listTitle => listTitle);
    }
   
    this.getDataFromToken();
  }
  getTitle(){
    var titlee = this.location.prepareExternalUrl(this.location.path());
    if(titlee.charAt(0) === '#'){
        titlee = titlee.slice( 1 );
    }

    for(var item = 0; item < this.listTitles.length; item++){
        if(this.listTitles[item].path === titlee){
            return this.listTitles[item].title;
        }
    }
    return 'Dashboard';
  }

  logoutUser(){
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  getDataFromToken(){
    this.userData=this.authService.DecodedToken;
  }

}
