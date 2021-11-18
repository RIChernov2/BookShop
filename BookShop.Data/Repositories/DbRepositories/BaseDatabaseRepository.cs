using System.Data;

namespace Data.Repositories.DbRepositories
{
    public abstract class BaseDatabaseRepository
    {
        protected IDbTransaction Transaction { get; }
        protected IDbConnection Connection => Transaction?.Connection;

        protected BaseDatabaseRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}