import { HttpClient } from "@angular/common/http";
import { Injectable, OnDestroy } from "@angular/core";
import { interval, Observable, of, ReplaySubject, Subscription } from "rxjs";
import { filter, switchMap, tap } from "rxjs/operators";
import { AuthenticationService } from "./authentication.service";
import { SnackBarService } from "./snack-bar.service";
import { User } from "../models/user.model";
import { ProfileNotificationSettings } from "../models/profile-notification-settings.model";
import { Message } from "../models/message.model";


@Injectable({
  providedIn: 'root'
})
export class NotificationsService implements OnDestroy {
  public hasNewNotifications: boolean = false;

  private notificationSettingsEmitter = new ReplaySubject<ProfileNotificationSettings | null>(1);
  public notificationSettings$ = this.notificationSettingsEmitter.asObservable();

  private notificationsEmitter = new ReplaySubject<Message[]>(1);
  public notifications$ = this.notificationsEmitter.asObservable();

  private currentUser: User | null = null;
  private subscription?: Subscription;
  private interval?: Subscription;
  private lastNotificationCheckDateMs: number;

  constructor(private httpClient: HttpClient,
              private authenticationService: AuthenticationService,
              private snackBarService: SnackBarService) {
    this.lastNotificationCheckDateMs = +Date.now();
    this.subscription = this.authenticationService.currentUser$.pipe(
      filter(user => user?.userId !== this.currentUser?.userId),
      switchMap(user => this.onUserChange(user))
    ).subscribe(settings => this.notificationSettingsEmitter.next(settings));
  }

  public updateNotificationSubscription(): void {
    this.interval?.unsubscribe();
    if (this.currentUser !== null) {
      this.getNotifications(this.currentUser!.userId).subscribe(messages => this.notificationsEmitter.next(messages));
      this.interval = interval(10000).pipe(
        switchMap(() => this.getNotifications(this.currentUser!.userId))
      ).subscribe(messages => this.notificationsEmitter.next(messages));
    }
  }

  public getSettings(userId: number): Observable<ProfileNotificationSettings> {
    return this.httpClient.get<ProfileNotificationSettings>(`notification-settings/get-by-user-id?userId=${userId}`);
  }

  public getNotifications(userId: number): Observable<Message[]> {
    return this.httpClient.get<Message[]>(`messages/get-by-user-and-settings?userId=${userId}`).pipe(
      tap(messages => {
        if (messages.length > 0) {
          const lastMessageDateMs = +new Date(messages[messages.length - 1].date);
          if (lastMessageDateMs >= this.lastNotificationCheckDateMs) {
            this.hasNewNotifications = true;
            this.lastNotificationCheckDateMs = lastMessageDateMs + 1;
            this.snackBarService.showSuccess('You have new notifications!', 2000);
          }
        }
      })
    );
  }

  public updateSettings(nextSettings: ProfileNotificationSettings): Observable<number> {
    nextSettings.email = this.currentUser?.email || '';
    return this.httpClient.post<number>('notification-settings/update', nextSettings)
      .pipe(tap(() => {
        this.snackBarService.showSuccess('Notification settings updated succesfully.', 2000);
        this.notificationSettingsEmitter.next(nextSettings);
        this.updateNotificationSubscription();
      }));
  }

  private onUserChange(user: User | null): Observable<ProfileNotificationSettings | null> {
    this.currentUser = user;
    this.updateNotificationSubscription();
    return user !== null ? this.getSettings(user.userId) : of(null);
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
    this.interval?.unsubscribe();
  }
}