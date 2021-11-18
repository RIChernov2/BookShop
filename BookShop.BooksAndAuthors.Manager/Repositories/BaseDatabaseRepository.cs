using BookShop.BooksAndAuthors.Manager.Configurations.Models;
using System.Data;

namespace BookShop.BooksAndAuthors.Manager.Repositories
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