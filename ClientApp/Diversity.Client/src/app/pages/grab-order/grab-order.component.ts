import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { DashboardService } from 'src/app/_core/_services/dashboard.service';
import { OrderService } from 'src/app/_core/_services/order.service';
import { ToastService } from 'src/app/_core/_services/toast-service.service';
import { environment } from 'src/environments/environment';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-grab-order',
  templateUrl: './grab-order.component.html',
  styleUrls: ['./grab-order.component.scss']
})
export class GrabOrderComponent implements OnInit {

  currentOrder: any = {};
  userBalanceAmount:number=0;
  fileBaseUrl=environment.fileBaseUrl;
  constructor(private orderService: OrderService, private spinner: NgxSpinnerService, private toastr: ToastService,private router:Router,private dashboardService:DashboardService) { }

  ngOnInit() {
    this.getCurrentUserOrder();
    this.getBalanceAmount();
  }

  imageObject:any[] = [];

  getCurrentUserOrder() {
    this.spinner.show();
    this.orderService.getCurrectOrder().subscribe({
      next: (response: any) => {
        if(response.order==null){
          this.toastr.error("You have no more pending orders.")
          this.spinner.hide();
          this.router.navigate(['/user/dashboard']);
        }
        else{
          this.currentOrder = response;
          this.spinner.hide();
          if(this.currentOrder.order && this.currentOrder.order.products.productImages.length>0){
            console.log("product images->"+this.currentOrder.order.products.productImages)
            this.currentOrder.order.products.productImages.forEach(element => {
              this.imageObject.push({image:this.fileBaseUrl+element.imagePath,thumbImage:this.fileBaseUrl+element.imagePath})
            });
          }
          else{
            this.imageObject=[];
          }
        }
      },
      error: (err: any) => {
        this.spinner.hide();
        this.toastr.error(err.error)
      }
    })
  }

  completeOrder() {
    if(this.currentOrder.order.products.amount>this.userBalanceAmount){
      this.toastr.error("Insufficient Balance. Please recharge your account.")
    }
    else{
      this.spinner.show();
      this.orderService.completeOrder(this.currentOrder.order.id).subscribe({
        next: (response: any) => {
          this.spinner.hide();
          this.toastr.success("Order Completed Successfully.");
          this.getCurrentUserOrder();
          this.loadUserDashboard();
          Swal.fire('Congrats', 'Order Completed Successfully', 'success')
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
