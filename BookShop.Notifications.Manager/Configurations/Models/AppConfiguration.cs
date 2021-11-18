
using BookShop.Notifications.Manager.Configurations.Models.Interfaces;
using NLog.Config;

namespace BookShop.Notifications.Manager.Configurations.Models
{
    public class AppConfiguration//: IAppConfiguration // добавил потестировать
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public string SchemaName { get; set; }
        public string FluentMigratorProfile { get; set; }
        public RabbitMq RabbitMq { get; set; }
        public MailSettings MailSettings { get; set; }
        public LoggingConfiguration NLogConfiguration { get; set; }
    }
}
