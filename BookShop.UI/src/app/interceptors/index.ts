import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ApiUrlInterceptor } from './api-url-interceptor';
import { JwtInterceptor } from './jwt-interceptor';
import { ErrorInterceptor } from './api-error-interceptor';

export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: ApiUrlInterceptor, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
];
