using System.Data;

namespace Infrastructure.Dapper
{
    public interface IDatabaseConnectionFactory
    {
        #region Methods
        Task<IDbConnection> CreateConnectionAsync();
        string GetSqlConnectionString();
        #endregion
    }
}