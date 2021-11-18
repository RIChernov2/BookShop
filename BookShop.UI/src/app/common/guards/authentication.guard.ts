import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { RouterParametersService } from '../services/router-parameters.service';


@Injectable({ providedIn: 'root' })
export class AuthenticationGuard implements CanActivate {
  constructor(private authenticationService: AuthenticationService,
              private routerParameterService: RouterParametersService) { }

  public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const currentUser = this.authenticationService.currentUserValue;
    if (currentUser) return true;
    // not logged in so redirect to login page with the return url
    this.routerParameterService.updateRoute(['login'], { returnUrl: state.url || null });
    return false;
  }
}