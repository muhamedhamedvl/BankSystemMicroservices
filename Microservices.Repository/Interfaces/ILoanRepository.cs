using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.Core.Models;
namespace Microservices.Repository.Interfaces
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllLoansAsync();
        Task<Loan?> GetLoanByIdAsync(int id);
        Task AddLoanAsync(Loan loan);
        Task UpdateLoanAsync(Loan loan);
        Task<Loan> DeleteLoanAsync(int id);
        Task<IEnumerable<Loan>> GetLoansByCustomerIdAsync(int customerId);
        Task<Loan?> GetLoanByNumberAsync(string loanNumber);
        Task<bool> LoanExistsAsync(string loanNumber);
    }
}
