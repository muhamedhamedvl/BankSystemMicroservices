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
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            return await _cardRepository.GetAllCardsAsync();
        }
        public async Task<Card?> GetCardByIdAsync(int id)
        {
            return await _cardRepository.GetCardByIdAsync(id);
        }
        public async Task AddCardAsync(Card card)
        {
            await _cardRepository.AddCardAsync(card);
        }
        public async Task UpdateCardAsync(Card card)
        {
            await _cardRepository.UpdateCardAsync(card);
        }
        public async Task DeleteCardAsync(int id)
        {
            await _cardRepository.DeleteCardAsync(id);
        }
        public async Task<IEnumerable<Card>> GetCardsByCustomerIdAsync(int customerId)
        {
            return await _cardRepository.GetCardsByCustomerIdAsync(customerId);
        }
        public async Task<Card?> GetCardByNumberAsync(string cardNumber)
        {
            return await _cardRepository.GetCardByNumberAsync(cardNumber);
        }
        public async Task<bool> CardExistsAsync(string cardNumber)
        {
            return await _cardRepository.CardExistsAsync(cardNumber);
        }
        public async Task<IEnumerable<Card>> GetCardsByAccountIdAsync(int accountId)
        {
            return await _cardRepository.GetCardsByAccountIdAsync(accountId);
        }
        public async Task<Card?> GetCardByAccountIdAsync(int accountId)
        {
            return await _cardRepository.GetCardByAccountIdAsync(accountId);
        }
        public async Task<IEnumerable<Card>> GetCardsByCardTypeAsync(string cardType)
        {
            return await _cardRepository.GetCardsByCardTypeAsync(cardType);
        }
        public async Task<IEnumerable<Card>> GetCardsByStatusAsync(string status)
        {
            return await _cardRepository.GetCardsByStatusAsync(status);
        }
        public async Task<IEnumerable<Card>> GetCardsByExpirationDateAsync(DateTime expirationDate)
        {
            return await _cardRepository.GetCardsByExpirationDateAsync(expirationDate);
        }
        public async Task<IEnumerable<Card>> GetCardsByCustomerIdAndStatusAsync(int customerId, string status)
        {
            return await _cardRepository.GetCardsByCustomerIdAndStatusAsync(customerId, status);

        }
    }
}
