import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { AuthenticationService } from "./authentication.service";
import { SnackBarService } from "./snack-bar.service";
import { User } from "../models/user.model";


@Injectable({
  providedIn: 'root'
})
export class UserInfoService {
  constructor(private httpClient: HttpClient,
              private authenticationService: AuthenticationService,
              private snackBarService: SnackBarService) { }

  public updateUser(user: User): Observable<number[]> {
    return this.httpClient.post<number[]>('users/update', user)
      .pipe(
        tap(() => {
          this.authenticationService.updateUser(user);
          this.snackBarService.showSuccess('User info successfully updated.');
        })
      );
  }
}