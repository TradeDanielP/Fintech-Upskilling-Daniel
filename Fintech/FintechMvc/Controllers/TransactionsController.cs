using Microsoft.AspNetCore.Mvc;
using FintechLibrary.DTOs;
using FintechLibrary.Services;
using FintechMvc.Models;
using System.Diagnostics;

namespace FintechMvc.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApiCallerService _apiService;

        // Constructor que recibe una instancia de ApiCallerService y la asigna a una variable privada
        public TransactionsController(ApiCallerService apiService)
        {
            _apiService = apiService;
        }

        // Método que maneja la solicitud GET para la acción Create
        public IActionResult Create()
        {
            try
            {
                // Devuelve la vista para crear una nueva transacción
                return View();
            }
            catch (Exception ex)
            {
                // En caso de excepción, devuelve la vista de error con el modelo de error
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        // Método que maneja la solicitud POST para la acción Create
        [HttpPost]
        public async Task<IActionResult> Create(TransactionDTO transaction)
        {
            try
            {
                // Verifica si el modelo es válido
                if (ModelState.IsValid)
                {
                    // Llama al servicio API para registrar una nueva transacción
                    await _apiService.RegisterTransactionAsync(transaction);
                    // Guarda un mensaje de éxito en TempData y redirige a la acción Index del controlador Accounts
                    TempData["Success"] = "La transaccion fue registrada exitosamente.";
                    return RedirectToAction("Index", "Accounts");
                }
                // Si el modelo no es válido, devuelve la vista con el modelo de transacción
                return View(transaction);
            }
            catch (Exception ex)
            {
                // En caso de excepción, guarda un mensaje de error en TempData y devuelve la vista de error con el modelo de error
                TempData["Error"] = "Ocurrió un error al registrar la transaccion.";
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        // Método que maneja la solicitud GET para la acción History
        public async Task<IActionResult> History(int accountId)
        {
            // Llama al servicio API para obtener el historial de transacciones de una cuenta específica
            var transactions = await _apiService.GetAsync<List<TransactionDTO>>($"transactions/history/{accountId}");
            // Devuelve la vista con la lista de transacciones
            return View(transactions);
        }

    }
}