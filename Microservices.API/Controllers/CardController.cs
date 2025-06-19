using Microservices.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microservices.Core.Models;
namespace Microservices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await _cardService.GetAllCardsAsync();
            return Ok(cards);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardById(int id)
        {
            var card = await _cardService.GetCardByIdAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _cardService.AddCardAsync(card);
            return CreatedAtAction(nameof(GetCardById), new { id = card.Id }, card);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(int id, [FromBody] Card card)
        {
            if (id != card.Id)
            {
                return BadRequest("Card ID mismatch.");
            }
            await _cardService.UpdateCardAsync(card);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _cardService.GetCardByIdAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            await _cardService.DeleteCardAsync(id);
            return NoContent();
        }
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCardsByCustomerId(int customerId)
        {
            var cards = await _cardService.GetCardsByCustomerIdAsync(customerId);
            if (cards == null || !cards.Any())
            {
                return NotFound();
            }
            return Ok(cards);
        }
        [HttpGet("number/{cardNumber}")]
        public async Task<IActionResult> GetCardByNumber(string cardNumber)
        {
            var card = await _cardService.GetCardByNumberAsync(cardNumber);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }
        [HttpGet("exists/{cardNumber}")]
        public async Task<IActionResult> CardExists(string cardNumber)
        {
            var exists = await _cardService.CardExistsAsync(cardNumber);
            return Ok(new { Exists = exists });
        }
        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetCardsByAccountId(int accountId)
        {
            var cards = await _cardService.GetCardsByAccountIdAsync(accountId);
            if (cards == null || !cards.Any())
            {
                return NotFound();
            }
            return Ok(cards);
        }
        [HttpGet("account/card/{accountId}")]
        public async Task<IActionResult> GetCardByAccountId(int accountId)
        {
            var card = await _cardService.GetCardByAccountIdAsync(accountId);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }
        [HttpGet("type/{cardType}")]
        public async Task<IActionResult> GetCardsByCardType(string cardType)
        {
            var cards = await _cardService.GetCardsByCardTypeAsync(cardType);
            if (cards == null || !cards.Any())
            {
                return NotFound();
            }
            return Ok(cards);
        }
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetCardsByStatus(string status)
        {
            var cards = await _cardService.GetCardsByStatusAsync(status);
            if (cards == null || !cards.Any())
            {
                return NotFound();
            }
            return Ok(cards);
        }
        [HttpGet("expiration/{expirationDate}")]
        public async Task<IActionResult> GetCardsByExpirationDate(DateTime expirationDate)
        {
            var cards = await _cardService.GetCardsByExpirationDateAsync(expirationDate);
            if (cards == null || !cards.Any())
            {
                return NotFound();
            }
            return Ok(cards);
        }
        [HttpGet("customer/{customerId}/status/{status}")]
        public async Task<IActionResult> GetCardsByCustomerIdAndStatus(int customerId, string status)
        {
            var cards = await _cardService.GetCardsByCustomerIdAndStatusAsync(customerId, status);
            if (cards == null || !cards.Any())
            {
                return NotFound();
            }
            return Ok(cards);
        }
    }
}
