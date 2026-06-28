namespace FinTrack.API.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsFixed { get; set; } = false;
        public int Installments { get; set; } = 1;
        public int InstallmentNumber { get; set; } = 1;
    }
}