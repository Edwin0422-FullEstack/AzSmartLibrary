using AzSmartLibrary.Application.DTOs;
using AzSmartLibrary.Application.Interfaces;
using AzSmartLibrary.Web.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AzSmartLibrary.Web.Controllers
{
    public class BooksController(IBookService _bookService, IAuthorService _authorService) : Controller
    {
        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllAsync();
            return View(books);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            // Arquitectura: Preparamos el ViewModel con los datos necesarios para renderizar
            var viewModel = new CreateBookViewModel
            {
                AuthorsList = await GetAuthorsSelectListAsync()
            };

            return View(viewModel);
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            // 1. Validación de Modelo (Annotations)
            if (!ModelState.IsValid)
            {
                // Patrón PRG (Post-Redirect-Get) safety:
                // Si falla, hay que recargar la lista porque HTTP es stateless (la lista se perdió)
                model.AuthorsList = await GetAuthorsSelectListAsync();
                return View(model);
            }

            try
            {
                // 2. Mapping Manual (ViewModel -> DTO)
                // En proyectos grandes usarías AutoMapper, pero aquí es explícito y limpio.
                var bookDto = new CreateBookDto(model.Title, model.AuthorId!.Value);

                await _bookService.CreateAsync(bookDto);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                // Error de Negocio (ej. duplicado)
                ModelState.AddModelError("Title", ex.Message);

                // Recarga obligatoria de la lista
                model.AuthorsList = await GetAuthorsSelectListAsync();
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeactivateAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Helper privado para encapsular la lógica de obtención de datos UI
        private async Task<SelectList> GetAuthorsSelectListAsync()
        {
            var authors = await _authorService.GetAllAsync();
            // SelectList(Source, ValueField, TextField, SelectedValue)
            return new SelectList(authors, "Id", "Name");
        }
    }
}