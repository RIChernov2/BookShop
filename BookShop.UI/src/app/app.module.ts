import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppCommonModule } from './common/app-common.module';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from './shared/shared.module';
import { httpInterceptorProviders } from './interceptors';

import { AppComponent } from './app.component';
import { RootPageComponent } from './pages/root-page/root-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { ContactsPageComponent } from './pages/contacts-page/contacts-page.component';


@NgModule({
  declarations: [
    AppComponent,
    RootPageComponent,
    LoginPageComponent,
    ContactsPageComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    AppCommonModule,
    HttpClientModule,
    NoopAnimationsModule,
    SharedModule
  ],
  providers: [
    httpInterceptorProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
