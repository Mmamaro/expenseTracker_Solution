using Dapper;
using expenseTracker_Api.Data;
using expenseTracker_Api.Models;
using System.Data;

namespace expenseTracker_Api.Repositories
{
    public class categoriesRepository
    {
        private readonly DapperContext _context;

        public categoriesRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {

            //Query
            const string sql = "select * from category";

            //Accessing the connection to the Db
            using IDbConnection connection = _context.createConnection();

            //Executing query using dapper
            var categories = await connection.QueryAsync<Category>(sql);

            return categories;

        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            //Getting the parameters I will later use
            var parameters = new { categoryId = id };

            //Accessing the connection to the Db
            using IDbConnection connection = _context.createConnection();

            //Query
            const string sql = "select * from category where categoryId = @categoryId";

            //Executing the Query
            var category = await connection.QueryFirstOrDefaultAsync<Category>(sql, parameters);

            return category;
        }

    }
}
