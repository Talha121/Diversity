import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ProductService } from 'src/app/_core/_services/product.service';
import { ToastService } from 'src/app/_core/_services/toast-service.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {
  productForm: FormGroup = new FormGroup({});
  productList: any[] = [];
  gridResult: any[] = [];
  currentproductId: any = 0;
  productStatus; any = "";
  selectedFiles: File[] = [];
  selectedData: any = {};
  constructor(private modalService: NgbModal, private productService: ProductService, private spinner: NgxSpinnerService, private toastr: ToastService) { }
  ngOnInit() {
    this.getproductRequestForUser();
  }

  ngOnDestroy() {
  }

  inititalizeProductForm() {
    this.productForm = new FormGroup({
      title: new FormControl("", Validators.required),
      amount: new FormControl("", Validators.required),
      orderNum: new FormControl("", Validators.required),
      estimatedReturn: new FormControl("", Validators.required),
      quantity: new FormControl("", Validators.required),
      commission: new FormControl("", Validators.required),
      description: new FormControl("", Validators.required)
    });
  }
  openProductCreateModal(content) {
    this.productForm.reset();
    this.inititalizeProductForm();
    this.selectedFiles = [];
    const modalRef = this.modalService.open(content, { size: 'lg', centered: true });
  }
  openProductEditModal(content, productId) {
    this.productForm.reset();
    this.inititalizeProductForm();
    this.selectedFiles = [];
    this.selectedData = this.productList.filter(x => x.id == productId)[0];
    this.patchForm(this.selectedData);
    const modalRef = this.modalService.open(content, { size: 'lg', centered: true });
  }

  onFileSelected(event) {

    const file: File[] = event.target.files;

    if (file.length > 0) {
      this.selectedFiles = file;
    }
  }

  addProduct() {
    if (this.productForm.valid && this.selectedFiles.length > 0) {
      const formData = new FormData();
      let data = this.productForm.value;
      Object.keys(data).forEach(key => formData.append(key, data[key]));
      for (var i = 0; i < this.selectedFiles.length; i++) {
        formData.append("productImages", this.selectedFiles[i]);
      }
      this.spinner.show();
      this.productService.createProduct(formData).subscribe({
        next: (productResponse: any) => {
          this.productForm.reset();
          this.modalService.dismissAll();
          this.spinner.hide();
          this.toastr.success("Product Added Successfully");
          this.getproductRequestForUser();
        },
        error: (err: any) => {
          this.spinner.hide();
          this.toastr.error(err.error);
        }
      })
    }
    else {
      let invalid = "";
      const controls = this.productForm.controls;
      for (const name in controls) {
        if (controls[name].invalid) {
          invalid = name + "," + invalid;
        }
      }
      if (this.selectedFiles.length == 0) {
        invalid = invalid + ", Images";
      }
      if (invalid != "") {
        this.toastr.error(invalid + " are required.")
      }
    }
  }

  getproductRequestForUser() {
    this.spinner.show();
    this.productService.getAllProducts().subscribe({
      next: (response: any) => {
        this.productList = response;
        this.gridResult = response;
        this.spinner.hide();
      },
      error: (err: any) => {
        this.toastr.error(err.error);
        this.spinner.hide();
      }
    })
  }

  updateProduct() {
    if (this.productForm.valid) {
      const formData = new FormData();
      let data = this.productForm.value;
      Object.keys(data).forEach(key => formData.append(key, data[key]));
      if (this.selectedFiles.length > 0) {
        for (var i = 0; i < this.selectedFiles.length; i++) {
          formData.append("productImages", this.selectedFiles[i]);
        }
      }
      formData.append("id", this.selectedData.id);
      this.spinner.show();
      this.productService.updateProduct(formData).subscribe({
        next: (productResponse: any) => {
          this.productForm.reset();
          this.modalService.dismissAll();
          this.spinner.hide();
          this.toastr.success("Product Added Successfully");
          this.getproductRequestForUser();
        },
        error: (err: any) => {
          this.spinner.hide();
          this.toastr.error(err.error);
        }
      })
    }
    else {
      let invalid = "";
      const controls = this.productForm.controls;
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
    console.log(data)
    if (data) {
      this.productForm.patchValue({
        title: data.title,
        amount: data.amount,
        orderNum: data.orderNum,
        estimatedReturn: data.estimatedReturn,
        quantity: data.quantity,
        commission: data.commission,
        description: data.description
      })
    }
  }
}

