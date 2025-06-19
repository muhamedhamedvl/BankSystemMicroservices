using Microservices.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Services.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<Loan>> GetAllLoansAsync();
        Task<Loan?> GetLoanByIdAsync(int id);
        Task AddLoanAsync(Loan loan);
        Task UpdateLoanAsync(Loan loan);
        Task DeleteLoanAsync(int id);
        Task<IEnumerable<Loan>> GetLoansByCustomerIdAsync(int customerId);
        Task<Loan?> GetLoanByNumberAsync(string loanNumber);
        Task<bool> LoanExistsAsync(string loanNumber);
    }
}
