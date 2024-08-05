namespace SmartCardSystem.SmartCardDTO
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string? SenderAccountNumber { get; set; }
        public string? ReceiverAccountNumber { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }
    }
}
