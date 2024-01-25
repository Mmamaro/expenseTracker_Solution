using Dapper;
using expenseTracker_Api.Data;
using expenseTracker_Api.Models;
using System.Data;

namespace expenseTracker_Api.Repositories
{
    public class expensesRepository
    {
        private readonly DapperContext _context;

        public expensesRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {

            //Query
            const string sql = "select * from expense";

            //Accessing the connection to the Db
            using IDbConnection connection = _context.createConnection();

            //Executing query using dapper
            var expenses = await connection.QueryAsync<Expense>(sql);

            return expenses;

        }

        public async Task<Expense> GetExpenseByIdAsync(int id)
        {
            //Getting the parameters I will later use
            var parameters = new { expenseId = id };

            //Accessing the connection to the Db
            using IDbConnection connection = _context.createConnection();

            //Query
            const string sql = "select * from expense where expenseId = @expenseId";

            //Executing the Query
            var expense = await connection.QueryFirstOrDefaultAsync<Expense>(sql, parameters);

            return expense;
        }

        public async Task AddExpenseAsync(Expense expenseObj)
        {
            try
            {
                //Addind parameters
                var parameters = new DynamicParameters();
                parameters.Add("userId", expenseObj.userId);
                parameters.Add("categoryId", expenseObj.categoryId);
                parameters.Add("amount", expenseObj.amount);
                parameters.Add("expenseName", expenseObj.expenseName);
                parameters.Add("date", expenseObj.date);

                //Accessing the connection
                using IDbConnection connection = _context.createConnection();

                //Query
                const string sql = $"insert into expense(userId, categoryId, amount, expenseName, date) " +
                                    $"values(@userId, @categoryId, @amount, @expenseName, @date)";

                //Executing Query
                await connection.ExecuteAsync(sql, parameters);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while trying to add an expense in the repo: {ex.Message}");
            }

        }

        public async Task UpdateExpenseAsync(Expense expenseObj)
        {
            try
            {
                //Addind parameters
                var parameters = new DynamicParameters();
                parameters.Add("expenseId", expenseObj.expenseId);
                parameters.Add("userId", expenseObj.userId);
                parameters.Add("categoryId", expenseObj.categoryId);
                parameters.Add("amount", expenseObj.amount);
                parameters.Add("expenseName", expenseObj.expenseName);
                parameters.Add("date", expenseObj.date);

                //Accessing the connection
                using IDbConnection connection = _context.createConnection();

                //Query
                const string sql = $"Update expense set userId = @userId, categoryId = @categoryId, " +
                                    $"amount = @amount, expenseName = @expenseName, date = @date where expenseId = @expenseId";

                //Executing Query
                await connection.ExecuteAsync(sql, parameters);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while trying to update an expense in the repo: {ex.Message}");
            }

        }

        public async Task DeleteExpenseAsync(int Id)
        {
            try
            {
                //Getting the Parameter
                var parameter = new { expenseId = Id };

                //Accessing connection
                using IDbConnection connection = _context.createConnection();

                //Query
                const string sql = "DELETE FROM expense WHERE expenseId = @expenseId";

                //Executing Query
                await connection.ExecuteAsync(sql, parameter);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while trying to delete an expense in the repo: {ex.Message}");
            }

        }

    }
}
