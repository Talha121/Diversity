import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { DepositComponent } from 'src/app/pages/deposit/deposit.component';
import { WithdrawComponent } from 'src/app/pages/withdraw/withdraw.component';
import { GrabOrderComponent } from 'src/app/pages/grab-order/grab-order.component';
import { OrderComponent } from 'src/app/pages/order/order.component';
import { ProfileComponent } from 'src/app/pages/profile/profile.component';
import { UserHeaderComponent } from 'src/app/pages/user-header/user-header.component';
import { AdminDepositComponent } from 'src/app/pages/admin-deposit/admin-deposit.component';
import { AdminWithdrawComponent } from 'src/app/pages/admin-withdraw/admin-withdraw.component';
import { AdminOrderComponent } from 'src/app/pages/admin-order/admin-order.component';
import { UserComponent } from 'src/app/pages/user/user.component';
import { ProductComponent } from 'src/app/pages/product/product.component';
import { UserGuard } from 'src/app/_core/_guards/user.guard';
import { AdminDashboardComponent } from 'src/app/pages/admin-dashboard/admin-dashboard.component';
import { AdminGuard } from 'src/app/_core/_guards/admin.guard';

export const AdminLayoutRoutes: Routes = [
    {
        path:'',
        redirectTo:'user/dashboard'
    },
    {
        path:'',
        redirectTo:'admin/admin-dashboard'
    },
    {
        path: 'user',
        component: UserHeaderComponent,
        canActivate:[UserGuard],
        children: [
            { path: 'dashboard', component: DashboardComponent },
            { path: 'deposit', component: DepositComponent },
            { path: 'withdraw', component: WithdrawComponent },
            { path: 'grab-order', component: GrabOrderComponent },
            { path: 'order', component: OrderComponent },
            { path: 'profile', component: ProfileComponent }
        ]
    },
    {
        path:'admin',
        canActivate:[AdminGuard],
        children:[
            {
                path: 'admin-dashboard',
                component: AdminDashboardComponent
            },
            {
                path: 'admin-deposit',
                component: AdminDepositComponent
            },
            {
                path: 'admin-withdraw',
                component: AdminWithdrawComponent
            },
            {
                path: 'admin-orders',
                component: AdminOrderComponent
            },
            {
                path: 'user',
                component: UserComponent
            },
            {
                path: 'product',
                component: ProductComponent
            },
        ]

    }

   

];
