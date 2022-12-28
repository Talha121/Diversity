import { Component, OnInit, TemplateRef } from '@angular/core';
import { ToastService } from 'src/app/_core/_services/toast-service.service';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  host: { class: 'toast-container position-fixed top-0 end-0 p-3', style: 'z-index: 1200' },
  styleUrls: ['./toast.component.scss']
})

export class ToastComponent  {
  show : boolean = true;

  constructor(public toastService: ToastService) {}

  isTemplate(toast) { 
    return toast.textOrTpl instanceof TemplateRef; 
  }

  close(toast){
    this.toastService.remove(toast);
  }

  hide(){
    this.show = false;
  }
}
