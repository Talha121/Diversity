import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
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
  userBalanceAmount: number = 0;
  userrechargerequired: number = 0;
  constructor(private orderService: OrderService, private spinner: NgxSpinnerService, private toastr: ToastService, private router: Router, private dashboardService: DashboardService,private cdr:ChangeDetectorRef) { }

  ngOnInit() {
    this.getCurrentUserOrder();
  }

  currentImageUrl='';
  imageObject: imageSlide[] = []
  

  getCurrentUserOrder() {
    this.imageObject=[];
    this.spinner.show();
    this.orderService.getCurrectOrder().subscribe({
      next: (response: any) => {
        if (response.res) {
          Swal.fire('KYC Not Approved Yet', 'Go to Proile and upload required documents to complete KYC Verification',  'error')
          this.spinner.hide();
          this.router.navigate(['/user/dashboard']);
          return
        }
        if (response.order == null) {
          this.toastr.error("You have no more pending orders.")
          this.spinner.hide();
          this.router.navigate(['/user/dashboard']);
        }
        else {
          this.currentOrder = response;
          this.spinner.hide();
          if (this.currentOrder.order && this.currentOrder.order.products.productImages.length > 0) {
            let data = this.currentOrder.order.products.productImages
            data.forEach(element => {
              var data = {
                image: element.imagePath,
                thumbImage: element.imagePath
              };
              this.imageObject.push(data)
            });
            this.currentImageUrl=this.imageObject[0].image;
          }
          else {
            this.imageObject = [];
          }
          this.getBalanceAmount();
          this.cdr.detectChanges();
        }
      },
      error: (err: any) => {
        this.spinner.hide();
        this.toastr.error(err.error)
      }
    })
  }

  completeOrder() {
    if (this.currentOrder.order.products.amount > this.userBalanceAmount) {
      Swal.fire(
        'Insufficient Balance',
        'Please Recharge $'+this.userrechargerequired+' to your Account',
        'info'
      )
      this.spinner.hide();
    }
    else {
      this.spinner.show();
      this.orderService.completeOrder(this.currentOrder.order.id).subscribe({
        next: (response: any) => {
          this.spinner.hide();
          this.toastr.success("Order Completed Successfully.");
          this.getCurrentUserOrder();
          Swal.fire('Order Completed Successfully', 'Funds have been added to your account', 'success').then((result) => {
            window.location.reload();
          })
        },
        error: (err: any) => {
          this.spinner.hide();
          this.toastr.error(err.error)
        }
      })
    }

  }

  getBalanceAmount() {
    this.dashboardService.getUserDashBoard();
    this.dashboardService.userDashboardData$.subscribe(response => {
      if (response != null) {
        this.userBalanceAmount = response.userAccountDetails?.balanceAmount;
        this.userrechargerequired = response.userAccountDetails?.rechargeAmount;
      }
    })
  }
  changeImage(imgUrl:any){
    this.currentImageUrl=imgUrl;
  }
}

interface imageSlide{
  image:string
  thumbImage:string
}
