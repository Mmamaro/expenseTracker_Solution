using Dapper;
using expenseTracker_Api.Data;
using expenseTracker_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace expenseTracker_Api.Repositories
{
    public class monthlySummariesRepository
    {
        private readonly DapperContext _context;

        public monthlySummariesRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<monthlySummary>> GetAllmonthlySummaryAsync()
        {

            //Query
            const string sql = "select * from monthlysummary";

            //Accessing the connection to the Db
            using IDbConnection connection = _context.createConnection();

            //Executing query using dapper
            var summaries = await connection.QueryAsync<monthlySummary>(sql);

            return summaries;

        }

        public async Task<monthlySummary> GetmonthlySummaryByIdAsync(int id)
        {
            //Getting the parameters I will later use
            var parameters = new { summaryId = id };

            //Accessing the connection to the Db
            using IDbConnection connection = _context.createConnection();

            //Query
            const string sql = "select * from monthlysummary where summaryId = @summaryId";

            //Executing the Query
            var summary = await connection.QueryFirstOrDefaultAsync<monthlySummary>(sql, parameters);

            return summary;
        }

        public async Task AddUserAsync(int userId, string uniqueIdentifier, string name, string year, string month, 
            decimal salary, decimal totalExpenses, decimal remainingBal, string email)
        {
            try
            {
                //Addind parameters
                var parameters = new DynamicParameters();
                parameters.Add("userId", userId);
                parameters.Add("uniqueIdentifier", uniqueIdentifier);
                parameters.Add("year", year);
                parameters.Add("month", month);
                parameters.Add("totalExpenses", totalExpenses);
                parameters.Add("remainingBal", remainingBal);
                parameters.Add("email", email);
                parameters.Add("name", name);
                parameters.Add("salary", salary);

                //Accessing the connection
                using IDbConnection connection = _context.createConnection();

                //Query
                const string sql = $"insert into monthlysummary(userId,uniqueIdentifier, name, year, month,salary, totalExpenses, remainingBal, email) " +
                                    $"values (@userId,@uniqueIdentifier,@name, @year, @month,@salary, @totalExpenses, @remainingBal, @email)";

                //Executing Query
                await connection.ExecuteAsync(sql, parameters);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while trying to add a user in the repo: {ex.Message}");
            }

        }

        public async Task<List<monthlySummary>> SearchByUserIdAsync(int userId) //Searching by any column name
        {
            //Getting the parameters I will later use
            var parameters = new { userId = userId };

            //Accessing the connection to the Db
            using IDbConnection connection = _context.createConnection();

            //Query
            string sql = "select * from monthlysummary where userId = userId";

            //Executing the Query
            var user = await connection.QueryAsync<monthlySummary>(sql, parameters);

            return user.ToList();
        }

    }
}
