import { Injectable, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { skip, tap } from 'rxjs/operators';
import { RouterParametersService } from './router-parameters.service';
import { User } from '../models/user.model';
import { LoginModel } from '../models/login.model';


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService implements OnDestroy {
  private currentUserEmitter: BehaviorSubject<User | null>;
  public currentUser$: Observable<User | null>;

  private subscription: Subscription;

  constructor(private httpClient: HttpClient,
              private routerParameterService: RouterParametersService) {
    const initialUser = localStorage.getItem('currentUser');
    this.currentUserEmitter = new BehaviorSubject<User | null>(initialUser ? JSON.parse(initialUser) : null);
    this.currentUser$ = this.currentUserEmitter.asObservable();
    this.subscription = this.currentUserEmitter.pipe(skip(1))
      .subscribe(user => this.updateLocalStorage(user));
  }

  public get currentUserValue(): User | null {
    return this.currentUserEmitter.value;
  }

  public login(model: LoginModel): Observable<User> {
    return this.httpClient.post<User>('authentication/login', model)
      .pipe(
        tap(user => this.currentUserEmitter.next(user))
      );
  }

  public logout(): void {
    if (this.currentUserValue !== null)
      this.currentUserEmitter.next(null);
  }
  
  public updateUser(user: User): void {
    this.currentUserEmitter.next(user);
  }

  private updateLocalStorage(user: User | null): void {
    if (user === null) {
      localStorage.removeItem('currentUser');
      this.routerParameterService.updateRoute(['login']);
    } else {
      const currentStoredUserJson = localStorage.getItem('currentUser');
      const currentStoredUser = currentStoredUserJson ? JSON.parse(currentStoredUserJson) : null;
      user.token = user.token || currentStoredUser?.token;
      localStorage.setItem('currentUser', JSON.stringify(user));
    }
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}