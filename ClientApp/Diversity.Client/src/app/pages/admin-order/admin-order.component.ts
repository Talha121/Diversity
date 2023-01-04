import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { OrderService } from 'src/app/_core/_services/order.service';
import { ToastService } from 'src/app/_core/_services/toast-service.service';
import { environment } from 'src/environments/environment';
@Component({
  selector: 'app-admin-order',
  templateUrl: './admin-order.component.html',
  styleUrls: ['./admin-order.component.scss']
})
export class AdminOrderComponent implements OnInit {

  orderList:any[]=[];
  gridResult:any[]=[];
  currentorderId:any=0;
  orderStatus;any="";
  constructor(private modalService: NgbModal, private orderService: OrderService,private spinner:NgxSpinnerService,private toastr: ToastService) { }
  ngOnInit() {
    this.getorderRequestForUser();
  }

  getorderRequestForUser(){
    this.spinner.show();
    this.orderService.getAllOrders().subscribe({
      next:(response:any)=>{
        this.orderList=response;
        this.gridResult=response;
        this.spinner.hide();
      },
      error:(err:any)=>{
        this.toastr.error(err.error);
        this.spinner.hide();
      }
    })
  }

  statusFilterChange(value:any){
    if(value!=""){
      this.gridResult=this.orderList.filter(x=>x.orderStatus==value);
    }
    else{
      this.gridResult=this.orderList;
    }
    
  }
}