import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { switchMap, take, tap } from "rxjs/operators";
import { CartManagementService } from "./cart-management.service";
import { NewOrderInfo, Order } from "../models/order.model";
import { SnackBarService } from "./snack-bar.service";
import { NotificationsService } from "./notifications.service";


@Injectable({
  providedIn: 'root'
})
export class OrdersManagementService {
  constructor(private httpClient: HttpClient,
              private cartManagementService: CartManagementService,
              private snackBarService: SnackBarService,
              private notificationsService: NotificationsService) { }

  public getOrdersByUserId(userId: number): Observable<Order[]> {
    return this.httpClient.get<Order[]>(`orders/get-by-user-id?userId=${userId}`);
  }

  public createNewOrder(userId: number, addressId: number): Observable<number[]> {
    return this.cartManagementService.cartProductsExtended$.pipe(
      take(1),
      switchMap(cartProducts => {
        const orderInfo = new NewOrderInfo({
          userId, addressId, cartProducts, cartId: cartProducts[0].cartId
        });
        return this.httpClient.post<number[]>(`orders/create`, orderInfo);
      }),
      tap(() => {
        this.cartManagementService.updateCartProducts([]);
        this.snackBarService.showSuccess('Order successfully created.');
        this.notificationsService.updateNotificationSubscription();
      })
    );
  }
}