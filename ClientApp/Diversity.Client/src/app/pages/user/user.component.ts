import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from 'src/app/_core/_services/toast-service.service';
import { UserService } from 'src/app/_core/_services/user.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  userList: any[] = [];
  gridResult: any[] = [];
  currentuserId: any = 0;
  userStatus; any = "";
  searchedValue: string = "";
  constructor(private modalService: NgbModal, private userService: UserService, private spinner: NgxSpinnerService, private toastr: ToastService) { }
  ngOnInit() {
    this.getuserRequestForUser();
  }

  ngOnDestroy() {
  }

  openChangeStatusModal(content, userId: any) {
    this.currentuserId = userId
    const modalRef = this.modalService.open(content, { size: 'sm', centered: true });
  }


  getuserRequestForUser() {
    this.spinner.show();
    this.userService.getAllUsers().subscribe({
      next: (response: any) => {
        if (response.length > 0) {
          response.forEach(element => {
            var currentOrderAmount=0;
            if (element.orders.length > 0) {
              var completedOrder = element.orders.filter(x => x.orderStatus == 'Completed').length;
              var pendingOrder = element.orders.filter(x => x.orderStatus == 'Pending');
              element['completedOrder'] = completedOrder;
              element['pendingOrder'] = pendingOrder.length;
              if(pendingOrder.length>0){
                currentOrderAmount=pendingOrder[0].products.amount;
              }
            }
            if (element.userAccount.length > 0) {
              if(element.userAccount[0].balanceAmount<currentOrderAmount){
                element['rechargeRequired']=currentOrderAmount-element.userAccount[0].balanceAmount;
              }
              else{
                element['rechargeRequired']=0;
              }
              element['balanceAmount']=element.userAccount[0].balanceAmount;
            }
          });

        }
        this.userList = response;
        this.gridResult = response;
        this.spinner.hide();
      },
      error: (err: any) => {
        this.toastr.error(err.error);
        this.spinner.hide();
      }
    })
  }

  searchFilterChange(val: any) {
    if (val != "") {
      let copy = JSON.stringify(this.userList)
      this.gridResult = JSON.parse(copy).filter((x: any) => {
        return x.name.toLowerCase().indexOf(val.toLowerCase()) >= 0;
      });
    }
    else {
      this.gridResult = this.userList;
    }

  }

}



