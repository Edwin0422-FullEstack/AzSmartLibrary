using AzSmartLibrary.Application.DTOs;
using AzSmartLibrary.Application.Interfaces;
using AzSmartLibrary.Web.Models; // Referencia al ViewModel
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
            return View(new CreateAuthorViewModel());
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                
                var authorDto = new CreateAuthorDto(model.Name);

                await _authorService.CreateAsync(authorDto);

                
                TempData["SuccessMessage"] = "Autor creado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                
                ModelState.AddModelError("Name", ex.Message);
                return View(model);
            }
        }

        // POST: Authors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _authorService.DeactivateAsync(id);
                TempData["SuccessMessage"] = "Autor desactivado correctamente.";
            }
            catch (Exception ex) // Captura genérica solo en capa UI final
            {
               
                TempData["ErrorMessage"] = $"Error al eliminar: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}