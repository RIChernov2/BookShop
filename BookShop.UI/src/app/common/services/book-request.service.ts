import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Book } from "../models/book.model";


@Injectable({
  providedIn: 'root'
})
export class BookRequestService {
  constructor(private httpClient: HttpClient) { }

  public getAllBooks(): Observable<Book[]> {
    return this.httpClient.get<Book[]>('books/get-all');
  }

  public getBooksByIds(ids: number[]): Observable<Book[]> {
    return this.httpClient.get<Book[]>(`books/get-by-ids?ids=${ids.join(',')}`);
  }
}