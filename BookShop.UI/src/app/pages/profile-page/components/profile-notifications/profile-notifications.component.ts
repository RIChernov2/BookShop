import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Message } from 'src/app/common/models/message.model';
import { ProfileNotificationSettings } from 'src/app/common/models/profile-notification-settings.model';
import { NotificationsService } from 'src/app/common/services/notifications.service';


@Component({
  selector: 'profile-notifications',
  templateUrl: './profile-notifications.component.html',
  styleUrls: ['./profile-notifications.component.scss']
})
export class ProfileNotificationsComponent implements OnInit, OnDestroy {
  public settingsFormGroup: FormGroup;
  public messages: Message[] = [];

  private currentSettings: ProfileNotificationSettings | null = null;
  private destroy$ = new Subject<void>();

  constructor(private notificationService: NotificationsService) {
    this.settingsFormGroup = new FormGroup({
      emailSettings: new FormGroup({
        info: new FormControl(false),
        warning: new FormControl(false),
        ads: new FormControl(false)
      }),
      pushSettings: new FormGroup({
        info: new FormControl(false),
        warning: new FormControl(false),
        ads: new FormControl(false)
      })
    });
    this.settingsFormGroup.disable();
  }

  ngOnInit(): void {
    this.notificationService.notificationSettings$.pipe(takeUntil(this.destroy$))
      .subscribe(settings => {
        this.currentSettings = settings;
        this.patchForm(settings);
        this.settingsFormGroup.enable();
      });
    this.notificationService.notifications$.pipe(takeUntil(this.destroy$))
      .subscribe(messages => this.messages = messages);
    this.notificationService.hasNewNotifications = false;
  }

  public onUpdate(): void {
    if (this.currentSettings === null) throw new Error("Error! Settings is null!");
    this.settingsFormGroup.disable();
    const nextSettings = new ProfileNotificationSettings({
      notificationSettingId: this.currentSettings.notificationSettingId,
      userId: this.currentSettings.userId,
      email: this.currentSettings.email,
      ...this.settingsFormGroup.value
    });
    this.notificationService.updateSettings(nextSettings).subscribe();
  }

  private patchForm(nextSettings: ProfileNotificationSettings | null): void {
    if (nextSettings === null) {
      this.settingsFormGroup.reset();
    } else {
      this.settingsFormGroup.patchValue({
        emailSettings: {
          info: nextSettings.emailSettings?.info,
          warning: nextSettings.emailSettings?.warning,
          ads: nextSettings.emailSettings?.ads,
        },
        pushSettings: {
          info: nextSettings.pushSettings?.info,
          warning: nextSettings.pushSettings?.warning,
          ads: nextSettings.pushSettings?.ads
        }
      });
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

}
