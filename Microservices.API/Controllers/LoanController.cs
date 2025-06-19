using Microservices.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microservices.Core.Models;
namespace Microservices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLoans()
        {
            var loans = await _loanService.GetAllLoansAsync();
            return Ok(loans);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanById(int id)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            return Ok(loan);
        }
        [HttpPost]
        public async Task<IActionResult> AddLoan([FromBody] Loan loan)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _loanService.AddLoanAsync(loan);
            return CreatedAtAction(nameof(GetLoanById), new { id = loan.Id }, loan);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoan(int id, [FromBody] Loan loan)
        {
            if (id != loan.Id)
            {
                return BadRequest("Loan ID mismatch.");
            }
            await _loanService.UpdateLoanAsync(loan);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            await _loanService.DeleteLoanAsync(id);
            return NoContent();

        }
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetLoansByCustomerId(int customerId)
        {
            var loans = await _loanService.GetLoansByCustomerIdAsync(customerId);
            if (loans == null || !loans.Any())
            {
                return NotFound();
            }
            return Ok(loans);
        }
        [HttpGet("{accountNumber}")]
        public async Task<IActionResult> GetLoanByAccountNumber(string accountNumber)
        {
            var loan = await _loanService.GetLoanByNumberAsync(accountNumber);
            if (loan == null)
            {
                return NotFound();
            }
            return Ok(loan);

        }
        [HttpGet("exists/{accountNumber}")]
        public async Task<IActionResult> LoanExists(string accountNumber)
        {
            var exists = await _loanService.LoanExistsAsync(accountNumber);
            return Ok(new { Exists = exists });
        }
    }
}
