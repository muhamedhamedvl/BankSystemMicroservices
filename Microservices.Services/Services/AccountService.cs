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
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAccountsAsync();
        }
        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            return await _accountRepository.GetAccountByIdAsync(id);
        }
        public async Task AddAccountAsync(Account account)
        {
            await _accountRepository.AddAccountAsync(account);
        }
        public async Task UpdateAccountAsync(Account account)
        {
            await _accountRepository.UpdateAccountAsync(account);
        }
        public async Task<Account> DeleteAccountAsync(int id)
        {
            return await _accountRepository.DeleteAccountAsync(id);
        }
        public async Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(int customerId)
        {
            return await _accountRepository.GetAccountsByCustomerIdAsync(customerId);
        }
        public async Task<Account?> GetAccountByNumberAsync(string accountNumber)
        {
            return await _accountRepository.GetAccountByNumberAsync(accountNumber);
        }
        public async Task<bool> AccountExistsAsync(string accountNumber)
        {
            return await _accountRepository.AccountExistsAsync(accountNumber);
        }
    }
}
