using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Core.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Range(100, double.MaxValue, ErrorMessage = "Loan amount must be at least 100.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Loan Type is required.")]
        [StringLength(50, ErrorMessage = "Loan Type cannot exceed 50 characters.")]
        public string LoanType { get; set; } = null!;

        [Required(ErrorMessage = "Loan Number is required.")]
        public string LoanNumber { get; set; }

        [Range(0, 100, ErrorMessage = "Interest rate must be between 0 and 100%.")]
        public float InterestRate { get; set; }

        [Range(1, 120, ErrorMessage = "Duration must be between 1 and 120 months.")]
        public int DurationMonths { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        public LoanStatus Status { get; set; } = LoanStatus.Pending;

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
    public enum LoanStatus
    {
        Pending,
        Approved,
        Rejected,
        Active,
        Completed
    }
}
