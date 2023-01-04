import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { DepositService } from 'src/app/_core/_services/deposit.service';
import { ToastService } from 'src/app/_core/_services/toast-service.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-deposit',
  templateUrl: './deposit.component.html',
  styleUrls: ['./deposit.component.scss']
})
export class DepositComponent implements OnInit {

  depositForm: FormGroup = new FormGroup({});
  selectedFile: any;
  depositList: any[] = [];
  gridResult: any[] = [];
  BankDetailImagePath: string = "";
  constructor(private modalService: NgbModal, private depositService: DepositService, private spinner: NgxSpinnerService, private toastr: ToastService) { }
  ngOnInit() {
    this.getBankDetails();
    this.getDepositRequestForUser();
    this.inititalizeDepositForm();
  }
  ngOnDestroy() {
  }

  inititalizeDepositForm() {
    this.depositForm = new FormGroup({
      amount: new FormControl("", Validators.required),
      type: new FormControl("", Validators.required),
      otherDetails: new FormControl(""),
    });
  }
  openDepositModal(content) {
    // this.modalService.open(content, { size: 'sm', centered: true });
    const modalRef = this.modalService.open(content, { size: 'lg', centered: true });
  }

  onFileSelected(event) {

    const file: File = event.target.files[0];

    if (file) {
      this.selectedFile = file;
    }
  }

  saveDeposit() {
    //debugger
    if (this.depositForm.valid && this.selectedFile) {
      let formValues = this.depositForm.value;
      const formData = new FormData();
      formData.append("proofFile", this.selectedFile);
      formData.append("amount", formValues.amount);
      formData.append("type", formValues.type);
      formData.append("otherDetails", formValues.otherDetails);
      this.spinner.show();
      console.log(formValues)
      this.depositService.createDepositRequest(formData).subscribe({
        next: (response: any) => {
          console.log(response)
          this.toastr.success("Deposit Request Created Successfully.");
          this.modalService.dismissAll();
          this.spinner.hide();
          this.getDepositRequestForUser();
        },
        error: (err: any) => {
          this.toastr.error(err.error);
          this.spinner.hide();
        }
      })
    }
    else {
      this.toastr.error("Amount, Type and Proof is required.");
    }
  }

  getDepositRequestForUser() {
    this.spinner.show();
    this.depositService.getDepositRequestForUser().subscribe({
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

  getBankDetails() {
    this.spinner.show()
    this.depositService.getBankDetails().subscribe({
      next: (response: any) => {
        this.spinner.hide();
        this.BankDetailImagePath = response.imagePath;
      },
      error: (err: any) => {
        this.spinner.hide();
        this.toastr.error(err.error)
      }
    })
  }
}
