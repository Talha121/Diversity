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
  constructor(private orderService: OrderService, private spinner: NgxSpinnerService, private toastr: ToastService, private router: Router, private dashboardService: DashboardService,private cdr:ChangeDetectorRef) { }

  ngOnInit() {
    this.getCurrentUserOrder();
    this.getBalanceAmount();
  }

  imageObject: imageSlide[] = [{image:'',thumbImage:''}]
  //     [{
  //     image: 'http://res.cloudinary.com/dokagygt7/image/upload/v1672852005/edprvasqs8ctf3k6zyjk.jpg',
  //     thumbImage: 'http://res.cloudinary.com/dokagygt7/image/upload/v1672852005/edprvasqs8ctf3k6zyjk.jpg',
  //     title: 'Hummingbirds are amazing creatures'
  // }, {
  //     image: 'https://sanjayv.github.io/ng-image-slider/contents/assets/img/slider/9.jpg',
  //     thumbImage: 'https://sanjayv.github.io/ng-image-slider/contents/assets/img/slider/9.jpg'
  // }, {
  //     image: 'https://sanjayv.github.io/ng-image-slider/contents/assets/img/slider/4.jpg',
  //     thumbImage: 'https://sanjayv.github.io/ng-image-slider/contents/assets/img/slider/4.jpg',
  //     title: 'Example with title.'
  // },{
  //     image: 'https://sanjayv.github.io/ng-image-slider/contents/assets/img/slider/7.jpg',
  //     thumbImage: 'https://sanjayv.github.io/ng-image-slider/contents/assets/img/slider/7.jpg',
  //     title: 'Hummingbirds are amazing creatures'
  // }, {
  //     image: 'https://sanjayv.github.io/ng-image-slider/contents/assets/img/slider/1.jpg',
  //     thumbImage: 'https://sanjayv.github.io/ng-image-slider/contents/assets/img/slider/1.jpg'
  // }, {
  //     image: 'https://sanjayv.github.io/ng-image-slider/contents/assets/img/slider/2.jpg',
  //     thumbImage: 'https://sanjayv.github.io/ng-image-slider/contents/assets/img/slider/2.jpg',
  //     title: 'Example two with title.'
  // }];

  getCurrentUserOrder() {
    debugger
    this.spinner.show();
    this.orderService.getCurrectOrder().subscribe({
      next: (response: any) => {
        if (response.res) {
          Swal.fire('Sorry', 'KYC Not Approved Yet', 'error')
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
          }
          else {
            this.imageObject = [];
          }
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
      this.toastr.error("Insufficient Balance. Please recharge your account.")
    }
    else {
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

  getBalanceAmount() {
    this.dashboardService.userDashboardData$.subscribe(response => {
      if (response != null) {
        this.userBalanceAmount = response.userAccountDetails?.balanceAmount;
      }
    })
  }
}

interface imageSlide{
  image:string
  thumbImage:string
}
