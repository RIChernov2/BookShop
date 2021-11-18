using BookShop.Notifications.Manager.Configurations.Models;
using System.Data;

namespace BookShop.Notifications.Manager.Repositories
{
    public abstract class BaseDatabaseRepository
    {
        protected IDbTransaction Transaction { get; }
        protected IDbConnection Connection => Transaction?.Connection;
        protected string SchemaName { get; set; }

        protected BaseDatabaseRepository(IDbTransaction transaction, AppConfiguration ñonfiguration)
        {
            Transaction = transaction;
            SchemaName = ñonfiguration.SchemaName;
        }
    }
}