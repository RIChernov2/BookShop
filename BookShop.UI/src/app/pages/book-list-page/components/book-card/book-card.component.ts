import { Component, Input, OnInit } from '@angular/core';
import { Book } from 'src/app/common/models/book.model';
import { CartManagementService } from 'src/app/common/services/cart-management.service';


@Component({
  selector: 'book-card',
  templateUrl: './book-card.component.html',
  styleUrls: ['./book-card.component.scss']
})
export class BookCardComponent implements OnInit {
  @Input() book: Book | undefined;
  @Input() canAddToCart: boolean = false;

  constructor(private cartManagementService: CartManagementService) { }

  ngOnInit(): void {
  }

  public onAddBook(): void {
    if (this.book)
      this.cartManagementService.addToCart(this.book);
  }

}
