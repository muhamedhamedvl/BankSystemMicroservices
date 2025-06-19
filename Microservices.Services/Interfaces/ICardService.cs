using Microservices.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Services.Interfaces
{
    public interface ICardService
    {
        Task<IEnumerable<Card>> GetAllCardsAsync();
        Task<Card?> GetCardByIdAsync(int id);
        Task AddCardAsync(Card card);
        Task UpdateCardAsync(Card card);
        Task DeleteCardAsync(int id);
        Task<IEnumerable<Card>> GetCardsByCustomerIdAsync(int customerId);
        Task<Card?> GetCardByNumberAsync(string cardNumber);
        Task<bool> CardExistsAsync(string cardNumber);
        Task<IEnumerable<Card>> GetCardsByAccountIdAsync(int accountId);
        Task<Card?> GetCardByAccountIdAsync(int accountId);
        Task<IEnumerable<Card>> GetCardsByCardTypeAsync(string cardType);
        Task<IEnumerable<Card>> GetCardsByStatusAsync(string status);
        Task<IEnumerable<Card>> GetCardsByExpirationDateAsync(DateTime expirationDate);
        Task<IEnumerable<Card>> GetCardsByCustomerIdAndStatusAsync(int customerId, string status);
    }
}
