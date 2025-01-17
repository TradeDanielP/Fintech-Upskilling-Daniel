using Microsoft.AspNetCore.Mvc;
using FintechLibrary.DTOs;
using FintechLibrary.Services;
using FintechMvc.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FintechMvc.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApiCallerService _apiService;

        // Constructor que recibe una instancia de ApiCallerService y la asigna a una variable privada
        public AccountsController(ApiCallerService apiService)
        {
            _apiService = apiService;
        }

        // Método que maneja la solicitud GET para la acción Index
        public async Task<IActionResult> Index()
        {
            // Llama al servicio API para obtener todas las cuentas
            var accounts = await _apiService.GetAllAccountsAsync();
            // Devuelve la vista con la lista de cuentas
            return View(accounts);
        }

        // Método que maneja la solicitud GET para la acción Create
        public IActionResult Create()
        {
            try
            {
                // Devuelve la vista para crear una nueva cuenta
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
        public async Task<IActionResult> Create(AccountDTO account)
        {
            // Verifica si el modelo es válido
            if (ModelState.IsValid)
            {
                // Llama al servicio API para crear una nueva cuenta
                await _apiService.PostAsync("api/accounts", account);
                // Guarda un mensaje de éxito en TempData y redirige a la acción Index
                TempData["Success"] = "La cuenta fue creada exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            // Si el modelo no es válido, guarda un mensaje de error en TempData y devuelve la vista con el modelo de cuenta
            TempData["Error"] = "Ocurrió un error al crear la cuenta.";
            return View(account);
        }

        // Métodos para Edit, Details, y Delete pueden seguir la misma estructura.
        public async Task<IActionResult> FilterByCustomer(string customerName)
        {
            // Llama al servicio API para filtrar cuentas por nombre de cliente
            var accounts = await _apiService.GetAsync<List<AccountDTO>>($"accounts/filter?customerName={customerName}");
            // Devuelve la vista Index con la lista de cuentas filtradas
            return View("Index", accounts);
        }

    }
}