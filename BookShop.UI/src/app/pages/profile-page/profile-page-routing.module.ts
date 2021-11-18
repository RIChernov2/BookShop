import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileCartComponent } from './components/profile-cart/profile-cart.component';
import { ProfileInfoComponent } from './components/profile-info/profile-info.component';
import { ProfileNotificationsComponent } from './components/profile-notifications/profile-notifications.component';
import { OrderCreateComponent } from './components/profile-orders/order-create/order-create.component';
import { OrderListComponent } from './components/profile-orders/order-list/order-list.component';
import { ProfilePageComponent } from './profile-page.component';


const routes: Routes = [
  {
    path: '',
    component: ProfilePageComponent,
    children: [
      {
        path: 'info',
        component: ProfileInfoComponent
      },
      {
        path: 'notifications',
        component: ProfileNotificationsComponent
      },
      {
        path: 'orders',
        children: [
          {
            path: 'list',
            component: OrderListComponent
          },
          {
            path: 'new',
            component: OrderCreateComponent
          }
        ]
      },
      {
        path: 'cart',
        component: ProfileCartComponent
      },
      {
        path: '',
        redirectTo: 'info',
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfilePageRoutingModule { }
