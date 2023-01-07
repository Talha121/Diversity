import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_core/_services/auth.service';

@Component({
  selector: 'app-site-page',
  templateUrl: './site-page.component.html',
  styleUrls: ['./site-page.component.scss']
})
export class SitePageComponent implements OnInit {

  isUserAuthenticated:boolean=false;
  tabName:string="Modinest"
  constructor(private authService:AuthService) { }

  ngOnInit() {
    this.isUserAuthenticated=this.authService.isAuthenticated;
  }

  onTabClick(value:any){
    this.tabName=value
  }
}
