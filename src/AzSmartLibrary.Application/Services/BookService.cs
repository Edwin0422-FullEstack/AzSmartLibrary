using AzSmartLibrary.Application.DTOs;
using AzSmartLibrary.Application.Interfaces;
using AzSmartLibrary.Application.Mappings;
using AzSmartLibrary.Core.Interfaces;

namespace AzSmartLibrary.Application.Services
{
    public class BookService(IBookRepository repository) : IBookService
    {
        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            // Usamos el método optimizado del repositorio que incluye Autores
            var books = await repository.GetAllWithAuthorsAsync();
            return books.Select(b => b.ToDto());
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await repository.GetByIdAsync(id);
            return book?.ToDto();
        }

        public async Task CreateAsync(CreateBookDto dto)
        {
            if (await repository.ExistsByTitleAsync(dto.Title))
            {
                throw new InvalidOperationException($"El libro '{dto.Title}' ya existe.");
            }

            var entity = dto.ToEntity();
            await repository.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdateBookDto dto)
        {
            if (await repository.ExistsByTitleAsync(dto.Title, dto.Id))
            {
                throw new InvalidOperationException($"El título '{dto.Title}' ya existe en otro libro.");
            }

            var entity = await repository.GetByIdAsync(dto.Id)
                         ?? throw new KeyNotFoundException("Libro no encontrado.");

            entity.UpdateEntity(dto);
            await repository.UpdateAsync(entity);
        }

        public async Task DeactivateAsync(int id)
        {
            await repository.DeactivateAsync(id);
        }
    }
}
