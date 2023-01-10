import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { DepositService } from 'src/app/_core/_services/deposit.service';
import { ToastService } from 'src/app/_core/_services/toast-service.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-admin-deposit',
  templateUrl: './admin-deposit.component.html',
  styleUrls: ['./admin-deposit.component.scss']
})
export class AdminDepositComponent implements OnInit {

  depositList: any[] = [];
  gridResult: any[] = [];
  currentDepositId: any = 0;
  accountNumber: string = "";
  accountTitle: string = "";
  depositStatus; any = "";
  selectedFile: any;
  constructor(private modalService: NgbModal, private depositService: DepositService, private spinner: NgxSpinnerService, private toastr: ToastService) { }
  ngOnInit() {
    this.getDepositRequestForUser();
  }

  ngOnDestroy() {
  }

  openChangeStatusModal(content, depositId: any) {
    this.currentDepositId = depositId
    const modalRef = this.modalService.open(content, { size: 'sm', centered: true });
  }
  openBankDetailModal(content) {
    const modalRef = this.modalService.open(content, { size: 'sm', centered: true });
  }

  saveDepositstatus() {
    //debugger
    if (this.depositStatus != "") {
      let data = {
        id: this.currentDepositId,
        status: this.depositStatus
      };
      this.spinner.show();
      this.depositService.updateDepositRequest(data).subscribe({
        next: (response: any) => {
          this.toastr.success("Deposit Approved Successfully.");
          this.modalService.dismissAll();
          this.spinner.hide();
          this.depositStatus = "";
          this.getDepositRequestForUser();
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

  getDepositRequestForUser() {
    this.spinner.show();
    this.depositService.getALlDepositRequest().subscribe({
      next: (response: any) => {
        this.depositList = response;
        this.gridResult = response;
        this.spinner.hide();
      },
      error: (err: any) => {
        this.toastr.error(err.error);
        this.spinner.hide();
      }
    })
  }

  statusFilterChange(value: any) {
    if (value != "") {
      this.gridResult = this.depositList.filter(x => x.status == value);
    }
    else {
      this.gridResult = this.depositList;
    }
  }

  onFileSelected(event) {

    const file: File = event.target.files[0];

    if (file) {
      this.selectedFile = file;
    }
  }

  saveBankDetail() {
    if (this.selectedFile && this.accountNumber != "" && this.accountTitle != "") {
      const formData = new FormData();
      formData.append("file", this.selectedFile);
      formData.append("accountNumber", this.accountNumber);
      formData.append("accountTitle", this.accountTitle)
      this.spinner.show();
      this.depositService.saveBankDetails(formData).subscribe({
        next: (response: any) => {
          this.spinner.hide();
          this.modalService.dismissAll();
          this.toastr.success("Bank Details Saved Successfully")
        },
        error: (err: any) => {
          this.spinner.hide();
          this.toastr.error(err.error)
        }
      })
    }
    else{
      this.toastr.error("Validation Failed")
    }
  }
}

