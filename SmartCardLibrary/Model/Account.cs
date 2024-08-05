namespace SmartCardLibrary.Model
{
    public class Account
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public string? AccountNumber { get; set; }
        public decimal Balance { get; set; }
        
    }
}
