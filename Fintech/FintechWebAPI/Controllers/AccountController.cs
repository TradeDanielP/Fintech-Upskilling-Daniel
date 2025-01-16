using Microsoft.AspNetCore.Mvc;
using FintechWebAPI.Models.DTOs;
using FintechWebAPI.Services;

namespace FintechWebAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            try
            {
                var accounts = await _accountService.GetAccounts();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            try
            {
                var account = await _accountService.GetAccount(id);
                return Ok(account);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDTO accountDTO)
        {
            var account = await _accountService.CreateAccount(accountDTO);
            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountDTO accountDTO)
        {
            try
            {
                var account = await _accountService.UpdateAccount(id, accountDTO);
                return Ok(account);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var result = await _accountService.DeleteAccount(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
