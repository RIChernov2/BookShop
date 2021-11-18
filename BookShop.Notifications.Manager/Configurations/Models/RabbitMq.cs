namespace BookShop.Notifications.Manager.Configurations.Models
{
    public class RabbitMq
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MessagesRequestQueue { get; set; }
        public string NSettingsRequestQueue { get; set; }
        public string PushRequestQueue { get; set; }
    }
}