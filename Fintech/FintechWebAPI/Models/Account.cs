namespace FintechWebAPI.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string Holder { get; set; }
        public decimal Balance { get; set; }
        public string AccountType { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
