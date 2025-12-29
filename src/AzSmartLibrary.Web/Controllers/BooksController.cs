using AzSmartLibrary.Application.DTOs;
using AzSmartLibrary.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AzSmartLibrary.Web.Controllers
{
    public class BooksController(IBookService _bookService, IAuthorService _authorService) : Controller
    {
        // GET: Books (Requisito: Mostrar lista de libros)
        public async Task<IActionResult> Index()
        {
            // El servicio ya trae el "AuthorName" populado gracias a nuestro Include optimizado
            var books = await _bookService.GetAllAsync();
            return View(books);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            // Paso crítico: Cargar la lista para el Dropdown
            await LoadAuthorsDropdown();
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                await LoadAuthorsDropdown(); // Recargar si hay error de validación UI
                return View(bookDto);
            }

            try
            {
                await _bookService.CreateAsync(bookDto);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                // Error de negocio (ej. Título duplicado)
                ModelState.AddModelError("Title", ex.Message);
                await LoadAuthorsDropdown(); // Recargar lista obligatoriamente
                return View(bookDto);
            }
        }

        // Acción para Soft Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeactivateAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Método privado helper para no repetir código (DRY)
        private async Task LoadAuthorsDropdown()
        {
            var authors = await _authorService.GetAllAsync();
            // SelectList(Fuente, ValorDelCampo, TextoAMostrar)
            ViewBag.Authors = new SelectList(authors, "Id", "Name");
        }
    }
}
