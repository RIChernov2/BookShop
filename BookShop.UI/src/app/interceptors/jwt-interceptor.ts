import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../common/services/authentication.service';


@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService) { }

  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const currentUser = this.authenticationService.currentUserValue;
    const isLoggedIn = currentUser && currentUser.token;
    if (isLoggedIn) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${ currentUser!.token }`
        }
      });
    }

    return next.handle(request);
  }
}