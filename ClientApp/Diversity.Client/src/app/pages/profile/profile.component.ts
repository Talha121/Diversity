import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from 'src/app/_core/_services/toast-service.service';
import { UserService } from 'src/app/_core/_services/user.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  selectedFile: any = {};
  imageSrc: any;
  userProfile: any = {};
  profileForm: FormGroup = new FormGroup({});
  kycForm: FormGroup = new FormGroup({});
  userKycData: any = {};
  userKycFrontImage: any;
  userKycBackImage: any;
  constructor(private userService: UserService, private spinner: NgxSpinnerService, private toastr: ToastService) { }

  ngOnInit() {
    this.inititalizeProfileForm()
    this.initializeKycForm()
    this.getUserProfile();
    this.getUserKyc();

  }

  inititalizeProfileForm() {
    this.profileForm = new FormGroup({
      name: new FormControl("", Validators.required),
      email: new FormControl(""),
      phoneNumber: new FormControl("", Validators.required)
    });
  }

  initializeKycForm() {
    this.kycForm = new FormGroup({
      identityType: new FormControl("", Validators.required),
      documentNumber: new FormControl("", Validators.required)
    });
  }

  readURL(event: any): void {
    if (event.target.files && event.target.files[0]) {
      const file = event.target.files[0];
      this.selectedFile = file;
      const reader = new FileReader();
      reader.onload = e => this.imageSrc = reader.result;
      reader.readAsDataURL(file);
    }
  }

  getUserProfile() {
    this.spinner.show();
    this.userService.getUserProfile().subscribe({
      next: (profile: any) => {
        this.spinner.hide();
        this.userProfile = profile;
        this.patchForm(this.userProfile);
      },
      error: (err: any) => {
        this.toastr.error(err.error);
      }
    })
  }

  updateProfile() {
    if (this.profileForm.valid) {
      let data = this.profileForm.value;
      this.userProfile.name = data.name;
      this.userProfile.phoneNumber = data.phoneNumber;
      const formData = new FormData();
      Object.keys(this.userProfile).forEach(key => formData.append(key, this.userProfile[key]));
      if (this.selectedFile) {
        formData.append("profileImage", this.selectedFile);
      }
      this.spinner.show();
      this.userService.updateUserProfile(formData).subscribe({
        next: (response: any) => {
          this.spinner.hide();
          this.toastr.success("Profile Updated Successfully");
          this.getUserProfile();
        },
        error: (err: any) => {
          this.spinner.hide();
          this.toastr.error(err.error);
        }
      })
    } {
      let invalid = "";
      const controls = this.profileForm.controls;
      for (const name in controls) {
        if (controls[name].invalid) {
          invalid = name + "," + invalid;
        }
      }
      if (invalid != "") {
        this.toastr.error(invalid + " are required.")
      }
    }
  }

  patchForm(data: any) {
    if (data) {
      this.profileForm.patchValue({
        name: data.name,
        email: data.email,
        phoneNumber: data.phoneNumber
      });
    }
  }
  getUserKyc() {
    this.spinner.show();
    this.userService.getUserKyc().subscribe({
      next: (response: any) => {
        this.userKycData = response;
        this.patchKycForm(this.userKycData);
        this.spinner.hide();
      },
      error: (err: any) => {
        this.spinner.hide();
        this.toastr.error(err.error)
      }
    })
  }
  CreateKyc() {
    if(this.kycForm.valid){
      const formData = new FormData();
      let data = this.kycForm.value;
      if(this.userKycData && this.userKycData !=null){
        data["id"]=this.userKycData.id;
        data["status"]="Pending"
      }
      else{
        data["status"]="Pending"
      }
      Object.keys(data).forEach(key => formData.append(key, data[key]));
      formData.append('documentImageOneFile',this.userKycFrontImage)
      formData.append('DocumentImageTwoFile',this.userKycBackImage)
      this.spinner.show();
      this.userService.createKyc(formData).subscribe({
        next: (response: any) => {
          this.userKycData = response;
          this.spinner.hide();
          this.getUserKyc();
        },
        error: (err: any) => {
          this.spinner.hide();
          this.toastr.error(err.error)
        }
      })
    }
    
  }

  onFrontFileSelected(event) {

    const file: File[] = event.target.files;

    if (file.length > 0) {
      this.userKycFrontImage = file[0];
    }
  }

  onBackFileSelected(event) {
    const file: File[] = event.target.files;
    if (file.length > 0) {
      this.userKycBackImage = file[0];
    }
  }

  patchKycForm(data: any) {
    if (data) {
      this.kycForm.patchValue({
        identityType: data.identityType,
        documentNumber: data.documentNumber
      });
    }
  }
}
