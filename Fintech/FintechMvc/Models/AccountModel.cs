using System.ComponentModel.DataAnnotations;

namespace FintechMvc.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(100)]
        public decimal Balance { get; set; }
        public string AccountType { get; set; }
        public ICollection<TransactionModel> Transactions { get; set; }
    }
}
