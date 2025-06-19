using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Core.Models
{
    public class Card
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Card number is required.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Card number must be 16 digits long.")]
        public string CardNumber { get; set; } = null!;

        [Required(ErrorMessage = "Cardholder name is required.")]
        [StringLength(100, ErrorMessage = "Cardholder name cannot exceed 100 characters.")]
        public string CardholderName { get; set; } = null!;

        [Required(ErrorMessage = "Expiration date is required.")]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "CVV is required.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV must be 3 digits long.")]
        public string CVV { get; set; } = null!;

        [Required(ErrorMessage = "Card Type is required.")]
        public CardType Type { get; set; }

        public bool IsActive { get; set; } = true;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        [Required]
        public int AccountId { get; set; }

        public Account Account { get; set; } = null!;
    }
    public enum CardType
    {
        Debit,
        Credit,
        Prepaid
    }
}
