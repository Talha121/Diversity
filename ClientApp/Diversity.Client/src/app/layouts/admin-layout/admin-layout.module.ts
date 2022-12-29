import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ClipboardModule } from 'ngx-clipboard';

import { AdminLayoutRoutes } from './admin-layout.routing';
import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { IconsComponent } from '../../pages/icons/icons.component';
import { MapsComponent } from '../../pages/maps/maps.component';
import { UserProfileComponent } from '../../pages/user-profile/user-profile.component';
import { TablesComponent } from '../../pages/tables/tables.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OrderComponent } from 'src/app/pages/order/order.component';
import { DepositComponent } from 'src/app/pages/deposit/deposit.component';
import { WithdrawComponent } from 'src/app/pages/withdraw/withdraw.component';
import { GrabOrderComponent } from 'src/app/pages/grab-order/grab-order.component';
import { ProfileComponent } from 'src/app/pages/profile/profile.component';
import { NgImageSliderModule } from 'ng-image-slider';

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
    UserProfileComponent,
    TablesComponent,
    IconsComponent,
    MapsComponent
  ]
})

export class AdminLayoutModule {}
