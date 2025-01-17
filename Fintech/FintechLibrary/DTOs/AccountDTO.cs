namespace FintechLibrary.DTOs
{
    public class AccountDTO
    {
        public string AccountNumber { get; set; }
        public string Holder { get; set; }
        public decimal Balance { get; set; }
        public string AccountType { get; set; }
    }

    public class AccountResponseDTO : AccountDTO
    {
        public int Id { get; set; }
        public ICollection<TransactionDTO> Transactions { get; set; }
    }
}
