import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomePageRoutingModule } from './book-list-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';

import { BookListComponent } from './book-list.component';
import { BookCardComponent } from './components/book-card/book-card.component';


@NgModule({
  declarations: [
    BookListComponent,
    BookCardComponent
  ],
  imports: [
    CommonModule,
    HomePageRoutingModule,
    SharedModule
  ]
})
export class BookListPageModule { }
