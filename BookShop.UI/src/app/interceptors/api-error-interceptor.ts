import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from '../common/services/authentication.service';
import { SnackBarService } from '../common/services/snack-bar.service';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService,
              private snackBarService: SnackBarService) { }

  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {
      if (err.status === 401) {
        // auto logout if 401 response returned from api
        this.authenticationService.logout();
        this.snackBarService.showError(err, "Invalid login and/or password. Please, try again.");
      } else {
        this.snackBarService.showError(err);
      }

      return throwError(err);
    }));
  }
}