import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from 'src/app/_core/_services/toast-service.service';
import { WithdrawService } from 'src/app/_core/_services/withdraw.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-admin-withdraw',
  templateUrl: './admin-withdraw.component.html',
  styleUrls: ['./admin-withdraw.component.scss']
})
export class AdminWithdrawComponent implements OnInit  {

  withdrawList:any[]=[];
  gridResult:any[]=[];
  currentwithdrawId:any=0;
  withdrawStatus;any="";
  constructor(private modalService: NgbModal, private withdrawService: WithdrawService,private spinner:NgxSpinnerService,private toastr: ToastService) { }
  ngOnInit() {
    this.getwithdrawRequestForUser();
  }

  ngOnDestroy() {
  }

  openChangeStatusModal(content,withdrawId:any) {
    this.currentwithdrawId=withdrawId
    const modalRef = this.modalService.open(content, { size: 'sm', centered: true });
  }

  savewithdrawstatus() {
    //debugger
    if (this.withdrawStatus !="") {
      let data={
        id:this.currentwithdrawId,
        status:this.withdrawStatus
      };
      this.spinner.show();
      this.withdrawService.updatewithdrawRequest(data).subscribe({
        next: (response: any) => {
          console.log(response)
          this.toastr.success("withdraw Approved Successfully.");
          this.modalService.dismissAll();
          this.spinner.hide();
          this.withdrawStatus="";
          this.getwithdrawRequestForUser();
        },
        error: (err: any) => {
          this.toastr.error(err.error);
          this.spinner.hide();
        }
      })
    }
    else {
      this.toastr.error("Select Valid Status");
    }
  }
  
  getwithdrawRequestForUser(){
    this.spinner.show();
    this.withdrawService.getAllwithdrawRequest().subscribe({
      next:(response:any)=>{
        this.withdrawList=response;
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
      this.gridResult=this.withdrawList.filter(x=>x.status==value);
    }
    else{
      this.gridResult=this.withdrawList;
    }
    
  }
}
