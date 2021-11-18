using NLog.Config;

namespace BookShop.Notifications.Manager.PushConsumer.Models
{
    public class AppConfiguration
    {
        public RabbitMq RabbitMq { get; set; }
        public LoggingConfiguration NLogConfiguration { get; set; }
    }
}
