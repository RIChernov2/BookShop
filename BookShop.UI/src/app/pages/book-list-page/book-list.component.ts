import { Component, OnInit } from '@angular/core';
import { BookRequestService } from 'src/app/common/services/book-request.service';
import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { Book } from 'src/app/common/models/book.model';
import { User } from 'src/app/common/models/user.model';


@Component({
  selector: 'book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss']
})
export class BookListComponent implements OnInit {
  public canAddToCart: boolean = false;
  public books: Book[] | undefined;

  constructor(private bookRequestService: BookRequestService,
              private authenticationService: AuthenticationService) { }

  ngOnInit(): void {
    this.authenticationService.currentUser$.subscribe(currentUser => this.canAddToCart = !!currentUser);
    this.bookRequestService.getAllBooks().subscribe(books => this.books = books);
  }
}
