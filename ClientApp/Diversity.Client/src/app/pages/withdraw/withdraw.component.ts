import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { DashboardService } from 'src/app/_core/_services/dashboard.service';
import { ToastService } from 'src/app/_core/_services/toast-service.service';
import { WithdrawService } from 'src/app/_core/_services/withdraw.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-withdraw',
  templateUrl: './withdraw.component.html',
  styleUrls: ['./withdraw.component.scss']
})
export class WithdrawComponent implements OnInit {

  withdrawForm: FormGroup = new FormGroup({});
  selectedFile: any;
  withdrawList:any[]=[];
  gridResult:any[]=[];
  userBalanceAmount:number=0;
  constructor(private modalService: NgbModal, private withdrawService: WithdrawService,private spinner:NgxSpinnerService,private toastr: ToastService,private dashboardService:DashboardService) { }
  ngOnInit() {
    this.getwithdrawRequestForUser();
    this.inititalizewithdrawForm();
    this.getBalanceAmount();
  }
  ngOnDestroy() {
  }

  inititalizewithdrawForm() {
    this.withdrawForm = new FormGroup({
      amount: new FormControl("", Validators.required),
      accountNumber: new FormControl("", Validators.required)
    });
  }
  openwithdrawModal(content) {
    // this.modalService.open(content, { size: 'sm', centered: true });
    const modalRef = this.modalService.open(content, { size: 'sm', centered: true });
  }

  savewithdraw() {
   
    if (this.withdrawForm.valid) {
      let formValues = this.withdrawForm.value;
      if(this.userBalanceAmount<formValues.amount){
        this.toastr.error("Insufficient Balance");
        return;
      }
      this.spinner.show();
      this.withdrawService.createwithdrawRequest(formValues).subscribe({
        next: (response: any) => {
          this.toastr.success("Withdraw Request Created Successfully.");
          this.modalService.dismissAll();
          this.spinner.hide();
          this.withdrawForm.reset();
          this.getwithdrawRequestForUser();
        },
        error: (err: any) => {
          this.toastr.error(err.error);
          this.spinner.hide();
        }
      })
    }
    else {
      this.toastr.error("Amount and Account number is required.");
    }
  }
  
  getwithdrawRequestForUser(){
    this.spinner.show();
    this.withdrawService.getwithdrawRequestForUser().subscribe({
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

  getBalanceAmount(){
    this.dashboardService.userDashboardData$.subscribe(response=>{
      if(response!=null){
        this.userBalanceAmount=response.userAccountDetails?.balanceAmount;
      }
    })
  }
}

