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

          
            modelBuilder.Entity<Author>().HasQueryFilter(a => a.IsActive);
            modelBuilder.Entity<Book>().HasQueryFilter(b => b.IsActive);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

      
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
