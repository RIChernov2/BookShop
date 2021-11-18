using System.Data;

namespace Data.Factories.Interfaces
{
    public enum DatabaseConnectionTypes
    {
        Default
    }
    
    public interface IDbConnectionFactory
    {
        IDbConnection CreateDbConnection(DatabaseConnectionTypes connectionType);
    }
}