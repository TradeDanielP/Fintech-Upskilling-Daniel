namespace FintechWebAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int SourceAccountId { get; set; }
        public int TargetAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string TransactionType { get; set; } // Ejemplo: "Transfer", "Deposit", "Withdrawal"
        
        // Relaciones de navegación
        public Account SourceAccount { get; set; }
        public Account TargetAccount { get; set; }
    }
}