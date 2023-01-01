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
  fileBaseUrl=environment.fileBaseUrl;
  constructor(private userService: UserService, private spinner: NgxSpinnerService, private toastr: ToastService) { }

  ngOnInit() {
    this.inititalizeProfileForm()
    this.getUserProfile();
    
  }

  inititalizeProfileForm() {
    this.profileForm = new FormGroup({
      name:new FormControl("", Validators.required),
      email: new FormControl(""),
      phoneNumber: new FormControl("", Validators.required)
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

  updateProfile(){
    if(this.profileForm.valid){
      let data= this.profileForm.value;
      this.userProfile.name=data.name;
      this.userProfile.phoneNumber=data.phoneNumber;
      const formData = new FormData();
      Object.keys(this.userProfile).forEach(key => formData.append(key, this.userProfile[key]));
      if(this.selectedFile) {
        formData.append("profileImage", this.selectedFile);
      }
      this.spinner.show();
      this.userService.updateUserProfile(formData).subscribe({
        next:(response:any)=>{
          this.spinner.hide();
          this.toastr.success("Profile Updated Successfully");
          this.getUserProfile();
        },
        error:(err:any)=>{
          this.spinner.hide();
          this.toastr.error(err.error);
        }
      })
    }{
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

  patchForm(data:any){
    if(data){
      this.profileForm.patchValue({
        name:data.name,
        email:data.email,
        phoneNumber:data.phoneNumber
      });
    }
  }
}
