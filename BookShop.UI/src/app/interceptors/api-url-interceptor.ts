import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';


@Injectable()
export class ApiUrlInterceptor implements HttpInterceptor {
  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const requestToApi = request.clone({ url: environment.apiUrl + request.url });
    return next.handle(requestToApi);
  }
}