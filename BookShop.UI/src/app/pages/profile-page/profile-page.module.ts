import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfilePageRoutingModule } from './profile-page-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';

import { ProfileInfoComponent } from './components/profile-info/profile-info.component';
import { ProfilePageComponent } from './profile-page.component';
import { ProfileNavigationComponent } from './components/profile-navigation/profile-navigation.component';
import { OrderListComponent } from './components/profile-orders/order-list/order-list.component';
import { ProfileCartComponent } from './components/profile-cart/profile-cart.component';
import { OrderCreateComponent } from './components/profile-orders/order-create/order-create.component';
import { CartProductListComponent } from './components/cart-product-list/cart-product-list.component';
import { ProfileNotificationsComponent } from './components/profile-notifications/profile-notifications.component';


@NgModule({
  declarations: [
    ProfileInfoComponent,
    ProfilePageComponent,
    ProfileNavigationComponent,
    OrderListComponent,
    ProfileCartComponent,
    OrderCreateComponent,
    CartProductListComponent,
    ProfileNotificationsComponent
  ],
  imports: [
    CommonModule,
    ProfilePageRoutingModule,
    SharedModule
  ]
})
export class ProfilePageModule { }
