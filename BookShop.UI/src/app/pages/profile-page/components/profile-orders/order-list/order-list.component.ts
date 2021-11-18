import { Component, OnDestroy, OnInit } from '@angular/core';
import { Order, OrderExtended } from 'src/app/common/models/order.model';
import { OrdersManagementService } from 'src/app/common/services/orders-management.service';
import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { Observable, of, Subscription } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { BookRequestService } from 'src/app/common/services/book-request.service';
import { OrderedBookExtended } from 'src/app/common/models/ordered-book.model';
import { Book } from 'src/app/common/models/book.model';
import { Address } from 'src/app/common/models/address.model';
import { User } from 'src/app/common/models/user.model';


@Component({
  selector: 'order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent implements OnInit, OnDestroy {
  public ordersExtended?: OrderExtended[];

  private subscription?: Subscription;

  constructor(private authenticationService: AuthenticationService,
              private ordersManagementService: OrdersManagementService,
              private bookRequestService: BookRequestService) { }

  ngOnInit(): void {
    this.subscription = this.authenticationService.currentUser$.pipe(
      switchMap(user => this.getOrdersExtendedByUser(user))
    ).subscribe(ordersExtended => this.ordersExtended = ordersExtended);
  }

  private getOrdersExtendedByUser(user: User | null): Observable<OrderExtended[]> {
    if (user === null) return of([]);

    let closureOrders: Order[];
    return this.ordersManagementService.getOrdersByUserId(user.userId).pipe(
      switchMap(orders => {
        closureOrders = orders;
        let bookIds = orders.flatMap(order => order.orderedBooks).map(book => book.bookId);
        bookIds = [...new Set(bookIds)];
        return this.bookRequestService.getBooksByIds(bookIds);
      }),
      map(books => this.convertToExtendedModel(closureOrders, books, user.addresses))
    );
  }

  private convertToExtendedModel(orders: Order[], books: Book[], addresses: Address[]): OrderExtended[] {
    return orders.map(order => {
      const orderedBooksExtended = order.orderedBooks.map(ob => {
        const book = books.find(b => b.bookId === ob.bookId);
        return new OrderedBookExtended(ob, book!);
      });
      const address = addresses.find(adr => adr.addressId === order.addressId);
      return new OrderExtended(order, address!, orderedBooksExtended);
    })
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

}
