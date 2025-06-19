using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Microservices.Core.Models
{
    public  class Account
    {
        public int Id { get;set; }

        [Required(ErrorMessage = "Account Number is required.")]
        [StringLength(20, ErrorMessage = "Account Number cannot exceed 20 characters.")]
        public string AccountNumber { get; set; } = null!;

        [Required(ErrorMessage = "Account Type is required.")]
        [StringLength(50, ErrorMessage = "Account Type cannot exceed 50 characters.")]
        public string AccountType { get; set; } = null!;

        [Required(ErrorMessage = "Balance is required.")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "Currency is required.")]
        public string Currency { get; set; } = null!;


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public ICollection<Transaction> TransactionsFrom { get; set; } = new List<Transaction>();
        public ICollection<Transaction> TransactionsTo { get; set; } = new List<Transaction>();
        public ICollection<Card> Cards { get; set; } = new List<Card>();
    }
    
    public enum AccountType
    {
        Savings,
        Current,
        FixedDeposit
    }
}
