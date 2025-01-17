using Microsoft.AspNetCore.Mvc;
using FintechWebAPI.Models.DTOs;
using FintechWebAPI.Services;

namespace FintechWebAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        // Inyección de dependencia del servicio de cuenta
        private readonly AccountService _accountService;

        // Constructor que recibe una instancia de AccountService
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        // Método para manejar la solicitud GET a /api/accounts
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            try
            {
                // Llama al servicio para obtener todas las cuentas
                var accounts = await _accountService.GetAccounts();
                // Devuelve una respuesta 200 OK con la lista de cuentas
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                // En caso de excepción, devuelve una respuesta 500 Internal Server Error con el mensaje de error
                return StatusCode(500, ex.Message);
            }
        }

        // Método para manejar la solicitud GET a /api/accounts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            try
            {
                // Llama al servicio para obtener una cuenta por ID
                var account = await _accountService.GetAccount(id);
                // Devuelve una respuesta 200 OK con la cuenta
                return Ok(account);
            }
            catch (KeyNotFoundException ex)
            {
                // En caso de excepción, devuelve una respuesta 404 Not Found con el mensaje de error
                return NotFound(ex.Message);
            }
        }

        // Método para manejar la solicitud POST a /api/accounts
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDTO accountDTO)
        {
            // Llama al servicio para crear una nueva cuenta
            var account = await _accountService.CreateAccount(accountDTO);
            // Devuelve una respuesta 201 Created con la cuenta creada
            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
        }

        // Método para manejar la solicitud PUT a /api/accounts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountDTO accountDTO)
        {
            try
            {
                // Llama al servicio para actualizar una cuenta por ID
                var account = await _accountService.UpdateAccount(id, accountDTO);
                // Devuelve una respuesta 200 OK con la cuenta actualizada
                return Ok(account);
            }
            catch (KeyNotFoundException ex)
            {
                // En caso de excepción, devuelve una respuesta 404 Not Found con el mensaje de error
                return NotFound(ex.Message);
            }
        }

        // Método para manejar la solicitud DELETE a /api/accounts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            // Llama al servicio para eliminar una cuenta por ID
            var result = await _accountService.DeleteAccount(id);
            // Si la cuenta no se encuentra, devuelve una respuesta 404 Not Found
            if (!result) return NotFound();
            // Si la cuenta se elimina exitosamente, devuelve una respuesta 204 No Content
            return NoContent();
        }
    }
}