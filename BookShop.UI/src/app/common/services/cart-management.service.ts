import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { combineLatest, Observable, of, ReplaySubject, Subscription } from 'rxjs';
import { map, switchMap, take } from 'rxjs/operators';
import { AuthenticationService } from './authentication.service';
import { BookRequestService } from './book-request.service';
import { CartProduct, CartProductExtended } from '../models/cart-product.model';
import { User } from '../models/user.model';
import { Book } from '../models/book.model';
import { Cart } from '../models/cart.model';


@Injectable({
  providedIn: 'root'
})
export class CartManagementService implements OnDestroy {
  private cartProductsExtendedEmitter = new ReplaySubject<CartProductExtended[]>(1);
  public cartProductsExtended$ = this.cartProductsExtendedEmitter.asObservable();

  private currentCart: Cart | null = null;
  private currentCartProducts: CartProductExtended[] = [];
  private currentUserSubscription: Subscription;

  constructor(private httpClient: HttpClient,
              private authenticationService: AuthenticationService,
              private bookRequestService: BookRequestService) {
    this.currentUserSubscription = this.authenticationService.currentUser$.pipe(
      switchMap(user => this.updateCart(user)),
      switchMap(cart => {
        this.currentCart = cart;
        return this.getCartProducts(cart)
      })
    ).subscribe(cartProducts => this.updateCartProducts(cartProducts));
  }

  public updateCartProducts(nextCartProducts?: CartProductExtended[]): void {
    if (nextCartProducts != null) this.currentCartProducts = nextCartProducts.slice();
    this.cartProductsExtendedEmitter.next(nextCartProducts ?? this.currentCartProducts);
  }

  public addToCart(book: Book): void {
    this.cartProductsExtended$.pipe(take(1))
      .subscribe(cartProducts => {
        const correspondingCartProduct = cartProducts.find(cp => cp.book.bookId == book.bookId);
        if (correspondingCartProduct) correspondingCartProduct.amount++;
        else cartProducts.push(new CartProductExtended({
          cartId: this.currentCart!.cartId,
          book,
          amount: 1
        }));
        
        this.updateCartProducts([...cartProducts]);
      });
  }

  public deleteFromCart(cartProductToDelete: CartProductExtended): void {
    this.cartProductsExtended$.pipe(take(1)).subscribe(cartProducts => {
      const correspondingCartProductIndex = cartProducts.findIndex(cp => cp.cartProductId == cartProductToDelete.cartProductId);
      if (correspondingCartProductIndex > -1) cartProducts.splice(correspondingCartProductIndex, 1);
      this.updateCartProducts([...cartProducts]);
    });
  }

  public saveCurrentCart(): Observable<number> {
    return this.cartProductsExtended$.pipe(
      take(1),
      switchMap(cProductsExtended => {
        const cProducts = cProductsExtended.map(cpExt => CartProduct.fromCartProductExtended(cpExt));
        return this.httpClient.post<number>(`cart-products/update?cartId=${this.currentCart!.cartId}`, cProducts)
      })
    );
  }

  private updateCart(user: User | null): Observable<Cart | null> {
    return user === null ? of(null) : this.getCart(user.userId);
  }

  private getCartProducts(cart: Cart | null): Observable<CartProductExtended[]> {
    return cart === null ? of([]) : this.getCartProductsExtended(cart.cartId);
  }

  private getCart(userId: number): Observable<Cart> {
    return this.httpClient.get<Cart>(`carts/get-by-user-id?userId=${userId}`);
  }

  private getCartProductsExtended(cartId: number): Observable<CartProductExtended[]> {
    let cartProductsClosure: CartProduct[];
    return this.httpClient.get<CartProduct[]>(`cart-products/get-by-cart-id?cartId=${cartId}`).pipe(
      switchMap(cartProducts => {
        cartProductsClosure = cartProducts;
        const bookIds = cartProducts.flatMap(cp => cp.bookId);
        return this.bookRequestService.getBooksByIds(bookIds);
      }),
      map(books => this.mapToExtendedModel(cartProductsClosure, books))
    );
  }

  private mapToExtendedModel(cartProducts: CartProduct[], books: Book[]): CartProductExtended[] {
    return cartProducts.map(cp => {
      const book = books.find(b => b.bookId === cp.bookId);
      if (book === undefined) throw new Error("Cart product can not have udefined book.")
      return CartProductExtended.fromCartProduct(cp, book);
    });
  }

  ngOnDestroy(): void {
    this.currentUserSubscription.unsubscribe();
  }
}
