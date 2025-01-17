using System.ComponentModel.DataAnnotations;

namespace FintechMvc.Models
{
    public class TransactionModel
    {
        [Required]
        public int SourceAccountId { get; set; }
        [Required]
        public int TargetAccountId { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public decimal Amount { get; set; }
        [Required]
        public string TransactionType { get; set; }
    }
}