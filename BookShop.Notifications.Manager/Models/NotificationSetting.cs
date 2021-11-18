
namespace BookShop.Notifications.Manager.Models
{
    public class NotificationSetting
    {
        public NotificationSetting()
        {
            EmailSettings = new NSettings();
            PushSettings = new NSettings();
        }
        public long NotificationSettingId { get; set; }
        public long UserId { get; set; }
        public NSettings EmailSettings { get; set; }
        public NSettings PushSettings { get; set; }
        public string Email { get; set; }
    }
}
