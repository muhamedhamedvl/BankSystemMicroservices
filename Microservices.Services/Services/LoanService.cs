using Microservices.Core.Models;
using Microservices.Repository.Interfaces;
using Microservices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Services.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }
        public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        {
            return await _loanRepository.GetAllLoansAsync();
        }
        public async Task<Loan?> GetLoanByIdAsync(int id)
        {
            return await _loanRepository.GetLoanByIdAsync(id);
        }
        public async Task AddLoanAsync(Loan loan)
        {
            await _loanRepository.AddLoanAsync(loan);
        }
        public async Task UpdateLoanAsync(Loan loan)
        {
            await _loanRepository.UpdateLoanAsync(loan);
        }
        public async Task DeleteLoanAsync(int id)
        {
            await _loanRepository.DeleteLoanAsync(id);
        }
        public async Task<IEnumerable<Loan>> GetLoansByCustomerIdAsync(int customerId)
        {
            return await _loanRepository.GetLoansByCustomerIdAsync(customerId);
        }
        public async Task<Loan?> GetLoanByNumberAsync(string loanNumber)
        {
            return await _loanRepository.GetLoanByNumberAsync(loanNumber);
        }
        public async Task<bool> LoanExistsAsync(string loanNumber)
        {
            return await _loanRepository.LoanExistsAsync(loanNumber);
        }
    }
}
