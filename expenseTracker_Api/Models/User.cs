using System.ComponentModel.DataAnnotations;

namespace expenseTracker_Api.Models
{
    public class User
    {
        public int userId { get; set; }
        [Required] public string name { get; set; }
        [Required] public string surname { get; set; }
        [Required][EmailAddress] public string email { get; set; }
        [Required] public decimal salary { get; set; }
    }
}
