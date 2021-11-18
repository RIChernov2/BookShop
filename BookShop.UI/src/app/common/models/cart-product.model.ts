import { Book } from "./book.model";

export class CartProduct {
  cartProductId: number = -1;
  cartId: number = -1;
  bookId: number = -1;
  amount: number = -1;

  constructor(init?: Partial<CartProduct>) {
    Object.assign(this, init);
  }

  public static fromCartProductExtended(cartProduct: CartProductExtended): CartProduct {
    const { book, ...props } = cartProduct;
    return new CartProduct({ bookId: book.bookId, ...props });
  }
}

export class CartProductExtended {
  cartProductId: number = -1;
  cartId: number = -1;
  book!: Book;
  amount: number = -1;

  constructor(init?: Partial<CartProductExtended>) {
    Object.assign(this, init);
  }

  public static fromCartProduct(cartProduct: CartProduct, book: Book): CartProductExtended {
    const { bookId, ...props } = cartProduct;
    return new CartProductExtended({ book, ...props });
  }
}