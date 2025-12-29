using AzSmartLibrary.Application.DTOs;
using AzSmartLibrary.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AzSmartLibrary.Web.Controllers
{
    public class AuthorsController(IAuthorService _authorService) : Controller
    {
        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAsync();
            return View(authors);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // Seguridad: Previene ataques CSRF
        public async Task<IActionResult> Create(CreateAuthorDto authorDto)
        {
            if (!ModelState.IsValid)
                return View(authorDto);

            try
            {
                await _authorService.CreateAsync(authorDto);
                // Patrón PRG (Post-Redirect-Get) para evitar reenvío de formulario
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                // Capturamos la validación de negocio (ej. "Nombre duplicado")
                ModelState.AddModelError("Name", ex.Message);
                return View(authorDto);
            }
        }

        // Acción extra para probar el Soft Delete más adelante
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _authorService.DeactivateAsync(id);
            }
            catch (Exception ex)
            {
                // En una app real, usaríamos TempData para mostrar el error en la vista
                // Por ahora, redirigimos simplemente
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
