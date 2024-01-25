using System.ComponentModel.DataAnnotations;

namespace expenseTracker_Api.Models
{
    public class Category
    {
        public int categoryId { get; set; }
        [Required] public string categoryname { get; set; }
    }
}
