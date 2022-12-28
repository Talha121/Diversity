import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { IconsComponent } from '../../pages/icons/icons.component';
import { MapsComponent } from '../../pages/maps/maps.component';
import { UserProfileComponent } from '../../pages/user-profile/user-profile.component';
import { TablesComponent } from '../../pages/tables/tables.component';
import { DepositComponent } from 'src/app/pages/deposit/deposit.component';
import { WithdrawComponent } from 'src/app/pages/withdraw/withdraw.component';
import { GrabOrderComponent } from 'src/app/pages/grab-order/grab-order.component';
import { OrderComponent } from 'src/app/pages/order/order.component';
import { ProfileComponent } from 'src/app/pages/profile/profile.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard',      component: DashboardComponent },
    { path: 'deposit',      component: DepositComponent },
    { path: 'withdraw',      component: WithdrawComponent },
    { path: 'grab-order',      component: GrabOrderComponent },
    { path: 'order',      component: OrderComponent },
    { path: 'profile',      component: ProfileComponent },
    { path: 'user-profile',   component: UserProfileComponent },
    { path: 'tables',         component: TablesComponent },
    { path: 'icons',          component: IconsComponent },
    { path: 'maps',           component: MapsComponent }
];
