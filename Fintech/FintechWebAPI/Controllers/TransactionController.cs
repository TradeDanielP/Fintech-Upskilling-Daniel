using Microsoft.AspNetCore.Mvc;
using FintechWebAPI.Models.DTOs;
using FintechWebAPI.Services;

namespace FintechWebAPI.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        // Inyección de dependencia del servicio de transacciones
        private readonly TransactionService _transactionService;

        // Constructor que recibe una instancia de TransactionService
        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Método para manejar la solicitud POST a /api/transactions/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionDTO transactionRequest)
        {
            // Llama al servicio para crear una nueva transacción
            var transaction = await _transactionService.CreateTransaction(transactionRequest);
            // Devuelve una respuesta 201 Created con la transacción creada
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
        }

        // Método para manejar la solicitud GET a /api/transactions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction(int id)
        {
            // Llama al servicio para obtener una transacción por ID
            var transaction = await _transactionService.GetTransaction(id);
            // Si la transacción no se encuentra, devuelve una respuesta 404 Not Found
            if (transaction == null) return NotFound();
            // Devuelve una respuesta 200 OK con la transacción
            return Ok(transaction);
        }

        // Método para manejar la solicitud GET a /api/transactions
        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            // Llama al servicio para obtener todas las transacciones
            var transactions = await _transactionService.GetTransactions();
            // Devuelve una respuesta 200 OK con la lista de transacciones
            return Ok(transactions);
        }

        // Método para manejar la solicitud PUT a /api/transactions/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] TransactionDTO transactionRequest)
        {
            // Llama al servicio para actualizar una transacción por ID
            var transaction = await _transactionService.UpdateTransaction(id, transactionRequest);
            // Si la transacción no se encuentra, devuelve una respuesta 404 Not Found
            if (transaction == null) return NotFound();
            // Devuelve una respuesta 200 OK con la transacción actualizada
            return Ok(transaction);
        }

        // Método para manejar la solicitud DELETE a /api/transactions/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            // Llama al servicio para eliminar una transacción por ID
            var result = await _transactionService.DeleteTransaction(id);
            // Si la transacción no se encuentra, devuelve una respuesta 404 Not Found
            if (!result) return NotFound();
            // Si la transacción se elimina exitosamente, devuelve una respuesta 204 No Content
            return NoContent();
        }
    }
}