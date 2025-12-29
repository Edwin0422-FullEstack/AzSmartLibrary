using AzSmartLibrary.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AzSmartLibrary.Infrastructure.Data
{
    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- 1. CONFIGURACIÓN DE SOFT DELETE (Global Query Filter) ---
            // Esto es "Magia" Senior: Automáticamente filtra los inactivos en TODA la app.
            modelBuilder.Entity<Author>().HasQueryFilter(a => a.IsActive);
            modelBuilder.Entity<Book>().HasQueryFilter(b => b.IsActive);

            // --- 2. RELACIONES (Fluent API explícito) ---
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                // Restrict: Evita borrar un autor si tiene libros (Protección DB nivel FK)
                .OnDelete(DeleteBehavior.Restrict);

            // --- 3. SEEDING (Datos de prueba iniciales) ---
            // Para que cuando corras la app no esté vacía (Mejora la experiencia de quien revisa la prueba)

            var authors = new List<Author>
        {
            new() { Id = 1, Name = "Gabriel García Márquez", IsActive = true },
            new() { Id = 2, Name = "J.K. Rowling", IsActive = true },
            new() { Id = 3, Name = "Isaac Asimov", IsActive = true }
        };

            modelBuilder.Entity<Author>().HasData(authors);

            modelBuilder.Entity<Book>().HasData(
                new { Id = 1, Title = "Cien Años de Soledad", AuthorId = 1, IsActive = true },
                new { Id = 2, Title = "El Amor en los Tiempos del Cólera", AuthorId = 1, IsActive = true },
                new { Id = 3, Title = "Harry Potter y la Piedra Filosofal", AuthorId = 2, IsActive = true },
                new { Id = 4, Title = "Fundación", AuthorId = 3, IsActive = true }
            );
        }
    }
}
