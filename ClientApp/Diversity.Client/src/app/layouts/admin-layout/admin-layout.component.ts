import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/_core/_services/dashboard.service';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {
  dashBoardData:any={};
  constructor(private dashboardService: DashboardService) { }

  ngOnInit() {
    this.loadUserDashboard();
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
