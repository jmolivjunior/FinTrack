namespace FinTrack.API.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;        public Decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Category { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
    }
}
