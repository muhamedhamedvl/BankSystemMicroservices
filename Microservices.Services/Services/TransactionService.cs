using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.Core.Models;
using Microservices.Repository.Interfaces;
using Microservices.Services.Interfaces;
namespace Microservices.Services.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepo;

        public TransactionService(ITransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public Task<IEnumerable<Transaction>> GetAllAsync() => _transactionRepo.GetAllAsync();

        public Task<Transaction?> GetByIdAsync(int id) => _transactionRepo.GetByIdAsync(id);

        public Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId) => _transactionRepo.GetByAccountIdAsync(accountId);

        public Task AddAsync(Transaction transaction) => _transactionRepo.AddAsync(transaction);

        public Task UpdateAsync(Transaction transaction) => _transactionRepo.UpdateAsync(transaction);

        public Task DeleteAsync(int id) => _transactionRepo.DeleteAsync(id);
    }
}
