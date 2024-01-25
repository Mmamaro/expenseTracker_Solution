using System.ComponentModel.DataAnnotations;

namespace expenseTracker_Api.Models
{
    public class Expense
    {
        public int expenseId { get; set; }
        [Required] public int userId { get; set; }
        [Required] public int categoryId { get; set; }
        [Required] public decimal amount { get; set; }
        [Required] public string expenseName { get; set; }
        [Required] public DateTime date { get; set; }
    }
}
