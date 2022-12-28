import { Injectable, TemplateRef } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ToastService {
  toasts: any[] = [];
  successoptions: any = { type:"Success",classname: 'bg-success text-light', delay: 10000};
  erroroptions: any = { type:"Error",classname: 'bg-danger text-light', delay: 10000000000 };
  warningoptions: any = { type:"Warning",classname: 'bg-warning text-light', delay: 10000 };
  

  remove(toast) {
    this.toasts = this.toasts.filter(t => t !== toast);
  }
  success(textOrTpl: string | TemplateRef<any>){

    this.toasts.push({ textOrTpl, ...this.successoptions });
  }
  error(textOrTpl: string | TemplateRef<any>){
    this.toasts.push({ textOrTpl, ...this.erroroptions });
  }
  warning(textOrTpl: string | TemplateRef<any>){
    this.toasts.push({ textOrTpl, ...this.warningoptions });
  }
}


// import { Injectable, TemplateRef } from '@angular/core';
// import { NgbToast, NgbToastService, NgbToastType } from 'ngb-toast';

// @Injectable({ providedIn: 'root' })
// export class ToastService {
 
//   constructor(private  toastService:  NgbToastService){

//   }
//   successToast: NgbToast = {
//     toastType:  NgbToastType.Success,
//     text:  "",
//     dismissible:  true,
//     timeInSeconds:5
//   }
//   errorToast: NgbToast = {
//     toastType:  NgbToastType.Danger,
//     text:  "",
//     dismissible:  true,
//     timeInSeconds:500
//   }
//   warningToast: NgbToast = {
//     toastType:  NgbToastType.Warning,
//     text:  "",
//     dismissible:  true,
//     timeInSeconds:5
//   }

//   public remove(toast:any) {
    
//   }
//   public success(textOrTpl: string){
//     this.successToast.text=textOrTpl;
//     this.toastService.show(this.successToast);
//   }
//   public error(textOrTpl: string){
//     this.errorToast.text=textOrTpl;
//     this.toastService.show(this.errorToast);
//   }
//   public warning(textOrTpl: string | TemplateRef<any>){
//   }
// }