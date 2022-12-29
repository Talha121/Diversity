import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
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
  constructor(private modalService: NgbModal, private withdrawService: WithdrawService,private spinner:NgxSpinnerService,private toastr: ToastService) { }
  ngOnInit() {
    this.getwithdrawRequestForUser();
    this.inititalizewithdrawForm();
  }
  ngOnDestroy() {
  }

  inititalizewithdrawForm() {
    this.withdrawForm = new FormGroup({
      amount: new FormControl("", Validators.required)
    });
  }
  openwithdrawModal(content) {
    // this.modalService.open(content, { size: 'sm', centered: true });
    const modalRef = this.modalService.open(content, { size: 'sm', centered: true });
  }

  savewithdraw() {
    //debugger
    if (this.withdrawForm.valid) {
      let formValues = this.withdrawForm.value;
      this.spinner.show();
      console.log(formValues)
      this.withdrawService.createwithdrawRequest(formValues).subscribe({
        next: (response: any) => {
          console.log(response)
          this.toastr.success("withdraw Request Created Successfully.");
          this.modalService.dismissAll();
          this.spinner.hide();
          this.getwithdrawRequestForUser();
        },
        error: (err: any) => {
          this.toastr.error(err.error);
          this.spinner.hide();
        }
      })
    }
    else {
      this.toastr.error("Amount is required.");
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
}

