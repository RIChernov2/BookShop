import { NotificationSettings } from "./notification-settings.model";

export class ProfileNotificationSettings {
  notificationSettingId: number = -1;
  userId: number = -1;
  emailSettings: NotificationSettings | null = null;
  pushSettings: NotificationSettings | null = null;
  email: string = '';

  constructor(init: Partial<ProfileNotificationSettings>) {
    Object.assign(this, init);
  }
}