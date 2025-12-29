using AzSmartLibrary.Core.Entities;
using AzSmartLibrary.Core.Interfaces;
using AzSmartLibrary.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AzSmartLibrary.Infrastructure.Repositories
{
    public class BookRepository(LibraryDbContext context) : IBookRepository
    {
        // --- LECTURA ---
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await context.Books
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllWithAuthorsAsync()
        {
            // REQUISITO CUMPLIDO: Traer datos del autor
            return await context.Books
                .Include(b => b.Author) // JOIN implícito
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await context.Books
                .Include(b => b.Author) // También útil al editar para mostrar el autor actual
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId)
        {
            return await context.Books
                .Where(b => b.AuthorId == authorId)
                .AsNoTracking()
                .ToListAsync();
        }

        // --- VALIDACIONES ---
        public async Task<bool> ExistsByTitleAsync(string title)
        {
            return await context.Books.AnyAsync(b => b.Title == title);
        }

        public async Task<bool> ExistsByTitleAsync(string title, int currentId)
        {
            return await context.Books.AnyAsync(b => b.Title == title && b.Id != currentId);
        }

        // --- ESCRITURA ---
        public async Task AddAsync(Book book)
        {
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            context.Books.Update(book);
            await context.SaveChangesAsync();
        }

        public async Task DeactivateAsync(int id)
        {
            var book = await context.Books.FindAsync(id);
            if (book != null)
            {
                book.IsActive = false;
                context.Books.Update(book);
                await context.SaveChangesAsync();
            }
        }
    }
}
