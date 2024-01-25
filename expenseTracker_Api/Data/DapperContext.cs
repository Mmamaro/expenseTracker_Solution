using MySqlConnector;
using System.Data;

namespace expenseTracker_Api.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection createConnection() => new MySqlConnection(_connectionString);
    }
}
