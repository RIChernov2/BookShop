import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { CartManagementService } from 'src/app/common/services/cart-management.service';
import { RouterParametersService } from 'src/app/common/services/router-parameters.service';
import { CartProductExtended } from 'src/app/common/models/cart-product.model';


@Component({
  selector: 'profile-cart',
  templateUrl: './profile-cart.component.html',
  styleUrls: ['./profile-cart.component.scss']
})
export class ProfileCartComponent implements OnInit, OnDestroy {
  public cartProducts: CartProductExtended[] = [];

  private subscription?: Subscription;

  constructor(private routerParameterService: RouterParametersService,
              private cartManagementService: CartManagementService) {}

  ngOnInit(): void {
    this.subscription = this.cartManagementService.cartProductsExtended$
      .subscribe(cProducts => this.cartProducts = cProducts)
  }

  public onOrderCreate(): void {
    this.routerParameterService.updateRoute(['profile/orders/new']);
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
