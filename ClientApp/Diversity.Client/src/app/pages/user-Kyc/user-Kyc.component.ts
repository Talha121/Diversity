import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from 'src/app/_core/_services/toast-service.service';
import { UserService } from 'src/app/_core/_services/user.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-Kyc',
  templateUrl: './user-Kyc.component.html',
  styleUrls: ['./user-Kyc.component.scss']
})
export class UserKycComponent implements OnInit {

  userKycList: any[] = [];
  gridResult: any[] = [];
  currentuserKycId: any = 0;
  userKycStatus; any = "";
  selectedFile: any;
  constructor(private modalService: NgbModal, private userKycService: UserService, private spinner: NgxSpinnerService, private toastr: ToastService) { }
  ngOnInit() {
    this.getKycRequestsForUser();
  }

  ngOnDestroy() {
  }

  openChangeStatusModal(content, userKycId: any) {
    this.currentuserKycId = userKycId
    const modalRef = this.modalService.open(content, { size: 'sm', centered: true });
  }

  saveuserKycstatus() {
    //debugger
    if (this.userKycStatus != "") {
      let data = {
        id: this.currentuserKycId,
        status: this.userKycStatus
      };
      this.spinner.show();
      this.userKycService.updateKycStatus(data).subscribe({
        next: (response: any) => {
          this.toastr.success("User Kyc Approved Successfully.");
          this.modalService.dismissAll();
          this.spinner.hide();
          this.userKycStatus = "";
          this.getKycRequestsForUser();
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

  getKycRequestsForUser() {
    this.spinner.show();
    this.userKycService.getAllUserKyc().subscribe({
      next: (response: any) => {
        this.userKycList = response;
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
      this.gridResult = this.userKycList.filter(x => x.status == value);
    }
    else {
      this.gridResult = this.userKycList;
    }
  }
}

