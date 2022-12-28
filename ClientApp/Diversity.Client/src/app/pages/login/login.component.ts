import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";
import { AuthService } from 'src/app/_core/_services/auth.service';
import { ToastService } from 'src/app/_core/_services/toast-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  constructor(private authService: AuthService,private router:Router,private spinner:NgxSpinnerService,private toastr: ToastService) { }
  loginForm: FormGroup = new FormGroup({});
  ngOnInit() {
    this.inititalizeLoginForm();
  }
  ngOnDestroy() {
  }

  inititalizeLoginForm() {
    this.loginForm = new FormGroup({
      email: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required)
    });
  }

  loginUser() {
    if (this.loginForm.valid) {
      this.spinner.show();
      this.authService.login(this.loginForm.value).subscribe({
        next: (response) => {
          this.authService.accesToken = response.res;
          this.spinner.hide();
          this.toastr.success(`Logged In Successfully.`);
          if(this.authService.isAuthenticated){
            this.router.navigate(['/dashboard']);
          }
        },
        error: (err) => {
          this.spinner.hide();
          this.toastr.error(err.error);
        },
      })
    }
    else{
      this.toastr.error("Validation Failed. Email and Password is required.");
    }
  }
}
