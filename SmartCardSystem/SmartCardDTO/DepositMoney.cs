namespace SmartCardSystem.SmartCardDTO
{
    public class DepositMoney
    {
        public int Id { get; set; }
        public string? AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime DepositDate { get; set; } = DateTime.Now;
    }
}
