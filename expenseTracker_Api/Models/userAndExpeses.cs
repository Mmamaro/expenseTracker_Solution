namespace expenseTracker_Api.Models
{
    public class userAndExpeses
    {
        public int userId { get; set; }
        public int expenseId { get; set; }
        public string email { get; set; }
        public decimal salary { get; set; }
        public DateTime date { get; set; }
    }
}
