import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ClipboardModule } from 'ngx-clipboard';

import { AdminLayoutRoutes } from './admin-layout.routing';
import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OrderComponent } from 'src/app/pages/order/order.component';
import { DepositComponent } from 'src/app/pages/deposit/deposit.component';
import { WithdrawComponent } from 'src/app/pages/withdraw/withdraw.component';
import { GrabOrderComponent } from 'src/app/pages/grab-order/grab-order.component';
import { ProfileComponent } from 'src/app/pages/profile/profile.component';
import { NgImageSliderModule } from 'ng-image-slider';
import { AdminDepositComponent } from 'src/app/pages/admin-deposit/admin-deposit.component';
import { AdminWithdrawComponent } from 'src/app/pages/admin-withdraw/admin-withdraw.component';
import { AdminOrderComponent } from 'src/app/pages/admin-order/admin-order.component';
import { ProductComponent } from 'src/app/pages/product/product.component';
import { UserComponent } from 'src/app/pages/user/user.component';
import { UserHeaderComponent } from 'src/app/pages/user-header/user-header.component';
import { UserGuard } from '../../_core/_guards/user.guard';
import { AdminDashboardComponent } from 'src/app/pages/admin-dashboard/admin-dashboard.component';
import { AdminGuard } from 'src/app/_core/_guards/admin.guard';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    HttpClientModule,
    NgbModule,
    ClipboardModule,
    ReactiveFormsModule,
    NgImageSliderModule    
  ],
  declarations: [
    DashboardComponent,
    OrderComponent,
    DepositComponent,
    WithdrawComponent,
    GrabOrderComponent,
    ProfileComponent,
    AdminDepositComponent,
    UserHeaderComponent,
    AdminWithdrawComponent,
    AdminOrderComponent,
    ProductComponent,
    UserComponent,
    AdminDashboardComponent
  ],
  providers:[
    UserGuard,
    AdminGuard
  ]
})

export class AdminLayoutModule {}
