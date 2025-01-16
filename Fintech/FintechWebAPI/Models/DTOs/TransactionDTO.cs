using System.ComponentModel.DataAnnotations;

namespace FintechWebAPI.Models.DTOs
{
    public class TransactionDTO
    {
        [Required]
        public int SourceAccountId { get; set; }
        [Required]
        public int TargetAccountId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string TransactionType { get; set; }
    }

    public class TransactionResponseDTO : TransactionDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}