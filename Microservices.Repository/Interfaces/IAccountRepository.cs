using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.Core.Models;
namespace Microservices.Repository.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByIdAsync(int id);
        Task AddAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task<Account> DeleteAccountAsync(int id);
        Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(int customerId);
        Task<Account?> GetAccountByNumberAsync(string accountNumber);
        Task<bool> AccountExistsAsync(string accountNumber);    
    }
}
