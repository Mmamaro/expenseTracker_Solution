using System.ComponentModel.DataAnnotations;

namespace expenseTracker_Api.Models
{
    public class monthlySummary
    {
        public int summaryId { get; set; }
        [Required] public int userId { get; set; }
        [Required] public string uniqueIdentifier { get; set; }
        [Required] public string name { get; set; }
        [Required] public string email { get; set; }
        [Required] public string year { get; set; }
        [Required] public string month { get; set; }
        [Required] public decimal salary { get; set; }
        [Required] public decimal totalExpenses { get; set; }
        [Required] public decimal remainingBal { get; set; }

    }
}
