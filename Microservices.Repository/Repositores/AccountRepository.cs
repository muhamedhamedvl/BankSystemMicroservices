using Microservices.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.Core.Models;
using Microservices.Repository.Data;
using Microsoft.EntityFrameworkCore;
namespace Microservices.Repository.Repositores
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }
        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task AddAccountAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
        public async Task<Account> DeleteAccountAsync(int id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return account;
        }
        public async Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(int customerId)
        {
            return await _context.Accounts.Where(a => a.CustomerId == customerId).ToListAsync();
        }
        public async Task<Account?> GetAccountByNumberAsync(string accountNumber)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }
        public async Task<bool> AccountExistsAsync(string accountNumber)
        {
            return await _context.Accounts.AnyAsync(a => a.AccountNumber == accountNumber);
        }
    }
}
