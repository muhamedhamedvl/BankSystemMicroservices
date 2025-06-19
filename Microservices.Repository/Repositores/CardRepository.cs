using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.Repository.Interfaces;
using Microservices.Core.Models;
using Microservices.Repository.Data;
using Microsoft.EntityFrameworkCore;
namespace Microservices.Repository.Repositores
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _Context;

        public CardRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            return await _Context.Cards.ToListAsync();
        }
        public async Task<Card?> GetCardByIdAsync(int id)
        {
            return await _Context.Cards.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task AddCardAsync(Card card)
        {
            await _Context.Cards.AddAsync(card);
            await _Context.SaveChangesAsync();
        }
        public async Task UpdateCardAsync(Card card)
        {
            _Context.Cards.Update(card);
            await _Context.SaveChangesAsync();
        }
        public async Task DeleteCardAsync(int id)
        {
            var card = await _Context.Cards.FirstOrDefaultAsync(c => c.Id == id);
            if (card == null)
            {
                throw new Exception("Card not found.");
            }
            _Context.Cards.Remove(card);
            await _Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Card>> GetCardsByCustomerIdAsync(int customerId)
        {
            return await _Context.Cards.Where(c => c.CustomerId == customerId).ToListAsync();
        }
        public async Task<Card?> GetCardByNumberAsync(string cardNumber)
        {
            return await _Context.Cards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
        }
        public async Task<bool> CardExistsAsync(string cardNumber)
        {
            return await _Context.Cards.AnyAsync(c => c.CardNumber == cardNumber);
        }
        public async Task<IEnumerable<Card>> GetCardsByAccountIdAsync(int accountId)
        {
            return await _Context.Cards.Where(c => c.AccountId == accountId).ToListAsync();
        }
        public async Task<Card?> GetCardByAccountIdAsync(int accountId)
        {
            return await _Context.Cards.FirstOrDefaultAsync(c => c.AccountId == accountId);
        }
        public async Task<IEnumerable<Card>> GetCardsByCardTypeAsync(string cardType)
        {
            return await _Context.Cards.Where(c => c.Type.ToString().Equals(cardType, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }
        public async Task<IEnumerable<Card>> GetCardsByStatusAsync(string status)
        {
            bool isActive = status.Equals("active", StringComparison.OrdinalIgnoreCase);
            return await _Context.Cards.Where(c => c.IsActive == isActive).ToListAsync();
        }
        public async Task<IEnumerable<Card>> GetCardsByExpirationDateAsync(DateTime expirationDate)
        {
            return await _Context.Cards.Where(c => c.ExpirationDate.Date == expirationDate.Date).ToListAsync();
        }
        public async Task<IEnumerable<Card>> GetCardsByCustomerIdAndStatusAsync(int customerId, string status)
        {
            bool isActive = status.Equals("active", StringComparison.OrdinalIgnoreCase);
            return await _Context.Cards.Where(c => c.CustomerId == customerId && c.IsActive == isActive).ToListAsync();
        }
    }
}
