import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {  Router } from '@angular/router';
import { AuthService } from 'src/app/_core/_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private authService: AuthService,private router:Router) { }
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

  registerUser() {
    if (this.registerForm.valid) {
      this.authService.register(this.registerForm.value).subscribe({
        next: (response:any) => {
          this.authService.accesToken = response.res;
          if(this.authService.isAuthenticated){
            this.router.navigate(['/']);
          }
        },
        error: (err) => {
          console.error('err', err);
        },
      })
    }
  }
}
