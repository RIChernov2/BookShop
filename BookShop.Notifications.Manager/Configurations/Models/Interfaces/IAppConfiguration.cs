using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Notifications.Manager.Configurations.Models.Interfaces
{
    public interface IAppConfiguration
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public string SchemaName { get; set; }
        public string FluentMigratorProfile { get; set; }
        public RabbitMq RabbitMq { get; set; }
    }
}
