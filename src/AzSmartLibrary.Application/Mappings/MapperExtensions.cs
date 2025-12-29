using AzSmartLibrary.Application.DTOs;
using AzSmartLibrary.Core.Entities;

namespace AzSmartLibrary.Application.Mappings
{
    public static class MapperExtensions
    {
        // --- AUTHOR  ---
        public static AuthorDto ToDto(this Author author)
            => new(author.Id, author.Name);

        public static Author ToEntity(this CreateAuthorDto dto)
            => new() { Name = dto.Name };

        public static void UpdateEntity(this Author author, UpdateAuthorDto dto)
        {
            author.Name = dto.Name;
        }

        // --- BOOK  ---
        public static BookDto ToDto(this Book book)
            => new(book.Id, book.Title, book.Author?.Name ?? "Desconocido", book.AuthorId);

        public static Book ToEntity(this CreateBookDto dto)
            => new() { Title = dto.Title, AuthorId = dto.AuthorId };

        public static void UpdateEntity(this Book book, UpdateBookDto dto)
        {
            book.Title = dto.Title;
            book.AuthorId = dto.AuthorId;
        }
    }
}
