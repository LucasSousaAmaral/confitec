using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Dapper
{
    public class SqlConnectionFactory : IDatabaseConnectionFactory
    {
        #region Properties
        private readonly string _connectionString;
        #endregion

        #region Constructors
        public SqlConnectionFactory(string connectionString)
            => _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        #endregion

        #region Methods
        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            await sqlConnection.OpenAsync();
            return sqlConnection;
        }
        public string GetSqlConnectionString() => _connectionString;
        #endregion
    }
}