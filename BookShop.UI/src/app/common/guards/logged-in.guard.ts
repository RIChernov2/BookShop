import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { RouterParametersService } from '../services/router-parameters.service';


@Injectable({ providedIn: 'root' })
export class LoggedInGuard implements CanActivate {
  constructor(private authenticationService: AuthenticationService,
              private routerParameterService: RouterParametersService) { }

  public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const currentUser = this.authenticationService.currentUserValue;
    if (currentUser) {
      this.routerParameterService.updateRoute(['profile']);
      return false;
    }
    return true;
  }
}