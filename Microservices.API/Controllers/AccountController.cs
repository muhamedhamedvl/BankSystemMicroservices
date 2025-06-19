using Microservices.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microservices.Core.Models;
namespace Microservices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return Ok(accounts);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }
        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] Account account)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _accountService.AddAccountAsync(account);
            return CreatedAtAction(nameof(GetAccountById), new { id = account.Id }, account);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] Account account)
        {
            if (id != account.Id)
            {
                return BadRequest("Account ID mismatch.");
            }
            await _accountService.UpdateAccountAsync(account);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            await _accountService.DeleteAccountAsync(id);
            return NoContent();
        }
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetAccountsByCustomerId(int customerId)
        {
            var accounts = await _accountService.GetAccountsByCustomerIdAsync(customerId);
            return Ok(accounts);
        }
        [HttpGet("number/{accountNumber}")]
        public async Task<IActionResult> GetAccountByNumber(string accountNumber)
        {
            var account = await _accountService.GetAccountByNumberAsync(accountNumber);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }
        [HttpGet("exists/{accountNumber}")]
        public async Task<IActionResult> AccountExists(string accountNumber)
        {
            var exists = await _accountService.AccountExistsAsync(accountNumber);
            return Ok(new { Exists = exists });
        }
    }
}
