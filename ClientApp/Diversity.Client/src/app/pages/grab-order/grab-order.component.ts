import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { DashboardService } from 'src/app/_core/_services/dashboard.service';
import { OrderService } from 'src/app/_core/_services/order.service';
import { ToastService } from 'src/app/_core/_services/toast-service.service';

@Component({
  selector: 'app-grab-order',
  templateUrl: './grab-order.component.html',
  styleUrls: ['./grab-order.component.scss']
})
export class GrabOrderComponent implements OnInit {

  currentOrder: any = {};
  userBalanceAmount:number=0;
  constructor(private orderService: OrderService, private spinner: NgxSpinnerService, private toastr: ToastService,private router:Router,private dashboardService:DashboardService) { }

  ngOnInit() {
    this.getCurrentUserOrder();
    this.getBalanceAmount();
  }

  imageObject = [{
    image: '../../../assets/images/black.png',
    thumbImage: '../../../assets/images/black.png',
  }, {
    image: '../../../assets/images/blue.png',
    thumbImage: '../../../assets/images/blue.png'
  }, {
    image: '../../../assets/images/red.png',
    thumbImage: '../../../assets/images/red.png',

  }];

  getCurrentUserOrder() {
    this.spinner.show();
    this.orderService.getCurrectOrder().subscribe({
      next: (response: any) => {
        if(response==null){
          this.toastr.error("You have no more pending orders.")
          this.spinner.hide();
          this.router.navigate(['/dashboard']);
        }
        else{
          this.currentOrder = response;
          this.spinner.hide();
        }
      },
      error: (err: any) => {
        this.spinner.hide();
        this.toastr.error(err.error)
      }
    })
  }

  completeOrder() {
    if(this.currentOrder.products.amount>this.userBalanceAmount){
      this.toastr.error("Insufficient Balance. Please recharge your account.")
    }
    else{
      this.spinner.show();
      this.orderService.completeOrder(this.currentOrder.id).subscribe({
        next: (response: any) => {
          this.spinner.hide();
          this.toastr.success("Order Completed Successfully.");
          this.getCurrentUserOrder();
          this.loadUserDashboard();
        },
        error: (err: any) => {
          this.spinner.hide();
          this.toastr.error(err.error)
        }
      })
    }
   
  }
  loadUserDashboard() {
    this.dashboardService.getUserDashBoard();
  }

  getBalanceAmount(){
    this.dashboardService.userDashboardData$.subscribe(response=>{
      if(response!=null){
        this.userBalanceAmount=response.userAccountDetails?.balanceAmount;
      }
    })
  }
}
