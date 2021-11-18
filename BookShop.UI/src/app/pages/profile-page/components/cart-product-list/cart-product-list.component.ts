import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { CartManagementService } from 'src/app/common/services/cart-management.service';
import { CartProductExtended } from 'src/app/common/models/cart-product.model';
import { BookRequestService } from 'src/app/common/services/book-request.service';


@Component({
  selector: 'cart-product-list',
  templateUrl: './cart-product-list.component.html',
  styleUrls: ['./cart-product-list.component.scss']
})
export class CartProductListComponent implements OnInit, OnDestroy {
  @Input() locked: boolean = false;

  public cartProducts: CartProductExtended[] = [];
  private subscription?: Subscription;

  constructor(private cartManagementService: CartManagementService) { }

  ngOnInit(): void {
    this.subscription = this.cartManagementService.cartProductsExtended$
      .subscribe(cpExtended => this.cartProducts = cpExtended)
  }

  public onCartProductDelete(cartProductExtended: CartProductExtended): void {
    this.cartManagementService.deleteFromCart(cartProductExtended);
  }

  public onAmountIncrease(cartProductExtended: CartProductExtended): void {
    cartProductExtended.amount++;
    this.cartManagementService.updateCartProducts();
  }

  public onAmountDecrease(cartProductExtended: CartProductExtended): void {
    if (cartProductExtended.amount == 1) this.onCartProductDelete(cartProductExtended);
    else {
      cartProductExtended.amount--;
      this.cartManagementService.updateCartProducts();
    }
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

}
