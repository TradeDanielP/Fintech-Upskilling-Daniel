using Microsoft.AspNetCore.Mvc;
using FintechWebAPI.Models.DTOs;
using FintechWebAPI.Services;

namespace FintechWebAPI.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionDTO transactionRequest)
        {
            var transaction = await _transactionService.CreateTransaction(transactionRequest);
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction(int id)
        {
            var transaction = await _transactionService.GetTransaction(id);
            if (transaction == null) return NotFound();
            return Ok(transaction);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _transactionService.GetTransactions();
            return Ok(transactions);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] TransactionDTO transactionRequest)
        {
            var transaction = await _transactionService.UpdateTransaction(id, transactionRequest);
            if (transaction == null) return NotFound();
            return Ok(transaction);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var result = await _transactionService.DeleteTransaction(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}