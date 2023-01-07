import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {  Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthService } from 'src/app/_core/_services/auth.service';
import { ToastService } from 'src/app/_core/_services/toast-service.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  termsConditionCheck:boolean=false;
  constructor(private modalService: NgbModal,private authService: AuthService,private router:Router,private spinner:NgxSpinnerService,private toastr: ToastService) { }
  registerForm: FormGroup = new FormGroup({});
  ngOnInit() {
    this.inititalizeRegisterForm();
  }
  ngOnDestroy() {
  }

  inititalizeRegisterForm() {
    this.registerForm = new FormGroup({
      name:new FormControl("", Validators.required),
      email: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required)
    });
  }

  openTermsAndConditionPopup(content){
    const modalRef = this.modalService.open(content, { size: 'lg', centered: true });
  }

  registerUser() {
    if (this.registerForm.valid) {
      this.spinner.show();
      this.authService.register(this.registerForm.value).subscribe({
        next: (response:any) => {
          this.spinner.hide()
          this.authService.accesToken = response.res;
          if(this.authService.isAuthenticated){
            this.router.navigate(['/']);
          }
         
        },
        error: (err) => {
          this.spinner.hide()
          this.toastr.error(err.error);
        },
      })
    }
  }
}
