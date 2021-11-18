namespace BookShop.Notifications.Manager.PushConsumer.Models
{
    public class RabbitMq
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PushRequestQueue { get; set; }
    }
}