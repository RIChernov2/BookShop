using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.BooksAndAuthors.Manager.Configurations.Models
{
    public class AppConfiguration
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public string FluentMigratorProfile { get; set; }
        public RabbitMq RabbitMq { get; set; }
        public string SchemaName { get; set; }
        public LoggingConfiguration NLogConfiguration { get; set; }
    }
}
