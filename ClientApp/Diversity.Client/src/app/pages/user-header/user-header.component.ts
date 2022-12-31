import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../_core/_services/dashboard.service';

@Component({
  selector: 'app-user-header',
  templateUrl: './user-header.component.html',
  styleUrls: ['./user-header.component.scss']
})
export class UserHeaderComponent implements OnInit {

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
