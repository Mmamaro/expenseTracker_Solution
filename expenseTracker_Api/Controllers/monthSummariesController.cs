using expenseTracker_Api.Models;
using expenseTracker_Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace expenseTracker_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class monthSummariesController : ControllerBase
    {
        private readonly monthlySummariesRepository _monthlySummariesRepository;

        public monthSummariesController(monthlySummariesRepository monthlySummariesRepository)
        {
            _monthlySummariesRepository = monthlySummariesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<monthlySummary>>> GetAllmonthlySummaries()
        {
            IEnumerable<monthlySummary> summaries = await _monthlySummariesRepository.GetAllmonthlySummaryAsync();

            if (summaries == null)
            {
                return BadRequest();
            }

            return Ok(summaries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<monthlySummary>> GetmonthlySummaryById(int id)
        {
            monthlySummary? summary = await _monthlySummariesRepository.GetmonthlySummaryByIdAsync(id);

            if (summary == null)
            {
                return NotFound();
            }

            return Ok(summary);
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchUser(int userId) // search by any column name
        {

            var monthlySummary = await _monthlySummariesRepository.SearchByUserIdAsync(userId);

            return Ok(monthlySummary);
        }
    }
}
