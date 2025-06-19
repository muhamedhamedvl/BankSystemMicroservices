using Microservices.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.Core.Models;
using Microservices.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Repository.Repositores
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _context;
        public LoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        {
            return await _context.Loans.ToListAsync();
        }
        public async Task<Loan?> GetLoanByIdAsync(int id)
        {
            return await _context.Loans.FirstOrDefaultAsync(l => l.Id == id);
        }
        public async Task AddLoanAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateLoanAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }
        public async Task<Loan> DeleteLoanAsync(int id)
        {
            var loan = await _context.Loans.FirstOrDefaultAsync(l => l.Id == id);
            if (loan == null)
            {
                throw new Exception("Loan not found.");
            }
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return loan;
        }
        public async Task<IEnumerable<Loan>> GetLoansByCustomerIdAsync(int customerId)
        {
            return await _context.Loans.Where(l => l.CustomerId == customerId).ToListAsync();
        }
        public async Task<Loan?> GetLoanByNumberAsync(string loanNumber)
        {
            return await _context.Loans.FirstOrDefaultAsync(l => l.LoanNumber == loanNumber);
        }
        public async Task<bool> LoanExistsAsync(string loanNumber)
        {
            return await _context.Loans.AnyAsync(l => l.LoanNumber == loanNumber);
        }
    }
}
