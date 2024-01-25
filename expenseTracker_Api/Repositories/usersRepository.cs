using expenseTracker_Api.Data;
using System.Data;
using System;
using expenseTracker_Api.Models;
using Dapper;

namespace expenseTracker_Api.Repositories
{
    public class usersRepository
    {
        private readonly DapperContext _context;

        public usersRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {

                //Query
                const string sql = "select * from user";

                //Accessing the connection to the Db
                using IDbConnection connection = _context.createConnection();

                //Executing query using dapper
                var users = await connection.QueryAsync<User>(sql);

                return users;

        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            //Getting the parameters I will later use
            var parameters = new {userId = id };

            //Accessing the connection to the Db
            using IDbConnection connection = _context.createConnection();

            //Query
            const string sql = "select * from user where userId = @userId";

            //Executing the Query
            var user = await connection.QueryFirstOrDefaultAsync<User>(sql, parameters);

            return user;
        }

        public async Task AddUserAsync(User userObj)
        {
            try
            {
                //Addind parameters
                var parameters = new DynamicParameters();
                parameters.Add("name", userObj.name, DbType.String);
                parameters.Add("surname", userObj.surname, DbType.String);
                parameters.Add("email", userObj.email, DbType.String);
                parameters.Add("salary", userObj.salary);

                //Accessing the connection
                using IDbConnection connection = _context.createConnection();

                //Query
                const string sql = "insert into user(name, surname, email, salary) values (@name, @surname, @email, @salary)";

                //Executing Query
                await connection.ExecuteAsync(sql, parameters);

            } catch(Exception ex)
            {
                Console.WriteLine($"Error while trying to add a user in the repo: {ex.Message}");
            }

        }

        public async Task UpdateUserAsync(User userObj)
        {
            try
            {
                //Addind parameters
                var parameters = new DynamicParameters();
                parameters.Add("userId", userObj.userId);
                parameters.Add("name", userObj.name, DbType.String);
                parameters.Add("surname", userObj.surname, DbType.String);
                parameters.Add("email", userObj.email, DbType.String);
                parameters.Add("salary", userObj.salary);

                //Accessing the connection
                using IDbConnection connection = _context.createConnection();

                //Query
                const string sql = "Update user set name = @name, surname = @surname, email = @email, salary = @salary where userId = @userId";

                //Executing Query
                await connection.ExecuteAsync(sql, parameters);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while trying to update a user in the repo: {ex.Message}");
            }

        }

        public async Task DeleteUserAsync(int Id)
        {
            try
            {
                //Getting the Parameter
                var parameter = new { userId = Id };

                //Accessing connection
                using IDbConnection connection = _context.createConnection();

                //Query
                const string sql = "DELETE FROM user WHERE userId = @userId";

                //Executing Query
                await connection.ExecuteAsync(sql, parameter);

            } catch(Exception ex)
            {
                Console.WriteLine($"Error while trying to delete a user in the repo: {ex.Message}");
            }

        }

        public async Task<List<User>> SearchUserAsync(string? email, string? name) //Searching by any column name
        {
            //Getting the parameters I will later use
            var parameters = new { email = email, name = name };

            //Accessing the connection to the Db
            using IDbConnection connection = _context.createConnection();

            //Query
            string sql = "select * from user";

            if (email != null)
            {
                sql = sql + " where email = @email";
            }

            if (name != null)
            {
                if (email != null)
                {
                    sql = sql + " and name = @name";
                }
                else
                {
                    sql = sql + " where name = @name";
                }
            }

            //Executing the Query
            var user = await connection.QueryAsync<User>(sql, parameters);

            return user.ToList();
        }
    }
}
