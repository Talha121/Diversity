import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/_core/_services/dashboard.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.scss']
})
export class AdminDashboardComponent implements OnInit {

  dashboardData: any = {};

  constructor(private dashboardService: DashboardService) { }

  ngOnInit() {
    this.getDashboardData();
  }

  getDashboardData() {
    this.dashboardService.getAdminDashboard().subscribe({
      next: (response: any) => {
        this.dashboardData = response
      },
      error: (err: any) => {

      }
    })
  }
}
