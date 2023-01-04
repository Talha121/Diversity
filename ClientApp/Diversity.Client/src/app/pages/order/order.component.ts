import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from 'src/app/_core/_services/toast-service.service';
import { environment } from 'src/environments/environment';
import { OrderService } from 'src/app/_core/_services/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {
  selectedFile: any;
  orderList:any[]=[];
  gridResult:any[]=[];
  constructor(private modalService: NgbModal, private orderService: OrderService,private spinner:NgxSpinnerService,private toastr: ToastService) { }
  ngOnInit() {
    this.getorderRequestForUser();
  }
  
  getorderRequestForUser(){
    this.spinner.show();
    this.orderService.getOrderForUser().subscribe({
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