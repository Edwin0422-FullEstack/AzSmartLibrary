using AzSmartLibrary.Core.Entities;
using AzSmartLibrary.Core.Interfaces;
using AzSmartLibrary.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AzSmartLibrary.Infrastructure.Repositories
{
    public class AuthorRepository(LibraryDbContext context) : IAuthorRepository
    {
        // --- LECTURA ---
        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await context.Authors
                .AsNoTracking()
                .OrderBy(a => a.Name) 
                .ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await context.Authors.FindAsync(id);
        }

        // --- VALIDACIONES ---
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await context.Authors.AnyAsync(a => a.Name == name);
        }

        public async Task<bool> ExistsByNameAsync(string name, int currentId)
        {
            
            return await context.Authors
                .AnyAsync(a => a.Name == name && a.Id != currentId);
        }

        public async Task<bool> HasBooksAsync(int authorId)
        {
            return await context.Books.AnyAsync(b => b.AuthorId == authorId);
        }

        // --- ESCRITURA ---
        public async Task AddAsync(Author author)
        {
            await context.Authors.AddAsync(author);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author author)
        {
            context.Authors.Update(author);
            await context.SaveChangesAsync();
        }

        public async Task DeactivateAsync(int id)
        {
            var author = await context.Authors.FindAsync(id);
            if (author != null)
            {
                author.IsActive = false;
               
                context.Authors.Update(author);
                await context.SaveChangesAsync();
            }
        }
    }
}
