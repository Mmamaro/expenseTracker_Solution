using expenseTracker_Api.Models;
using expenseTracker_Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace expenseTracker_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class expenseController : ControllerBase
    {
        private readonly expensesRepository _expensesRepository;

        public expenseController(expensesRepository expensesRepository)
        {
            _expensesRepository = expensesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetAllExpenses()
        {
            IEnumerable<Expense> expense = await _expensesRepository.GetAllExpensesAsync();

            if (expense == null)
            {
                return BadRequest();
            }

            return Ok(expense);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpenseById(int id)
        {
            Expense? expense = await _expensesRepository.GetExpenseByIdAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] Expense expenseObj)
        {
            if (expenseObj.userId <= 0 || expenseObj.categoryId <= 0 || expenseObj.amount <= 0 
                || string.IsNullOrWhiteSpace(expenseObj.expenseName))
            {
                return BadRequest();
            }

            await _expensesRepository.AddExpenseAsync(expenseObj);

            return Ok("Expense Added successfully");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateExpense([FromBody] Expense expenseObj)
        {
            if (expenseObj.userId <= 0 || expenseObj.categoryId <= 0 || expenseObj.amount <= 0 
                || string.IsNullOrWhiteSpace(expenseObj.expenseName))
            {
                return BadRequest();
            }

            await _expensesRepository.UpdateExpenseAsync(expenseObj);
            return Ok("Expense Updated successfully");
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteExpense(int Id)
        {

            if (Id == 0)
            {
                return NotFound();
            }

            await _expensesRepository.DeleteExpenseAsync(Id);

            return Ok("Expense deleted successfully");

        }
    }
}
