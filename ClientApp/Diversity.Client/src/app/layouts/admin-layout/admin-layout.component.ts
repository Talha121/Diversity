import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_core/_services/auth.service';
import { DashboardService } from 'src/app/_core/_services/dashboard.service';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {
  dashBoardData:any={};
  constructor(private dashboardService: DashboardService,private authService:AuthService) { }

  ngOnInit() {
    if(this.authService.DecodedToken.role=="User"){
      this.loadUserDashboard();
    }
  }

  loadUserDashboard() {
    this.dashboardService.getUserDashBoard();
    this.dashboardService.userDashboardData$.subscribe(response=>{
      if(response!=null){
        this.dashBoardData=response;
        console.log(this.dashBoardData)
      }
    })
  }
}
