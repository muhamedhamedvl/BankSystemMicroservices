using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Microservices.Core.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public string Reference { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Transaction Type is required.")]
        [StringLength(50, ErrorMessage = "Transaction Type cannot exceed 50 characters.")]
        public string TransactionType { get; set; } = null!;


        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Currency is required.")]
        [StringLength(3, ErrorMessage = "Currency must be 3 characters long.")]
        public string Currency { get; set; } = null!;

        [Required(ErrorMessage = "Account ID is required.")]
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;

        [Required(ErrorMessage = "Transaction Date is required.")]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int FromAccountId { get; set; }
        public Account FromAccount { get; set; } = null!;

        public int? ToAccountId { get; set; }
        public Account? ToAccount { get; set; }
    }
}
