using AzSmartLibrary.Application.DTOs;
using AzSmartLibrary.Application.Interfaces;
using AzSmartLibrary.Application.Mappings;
using AzSmartLibrary.Core.Interfaces;

namespace AzSmartLibrary.Application.Services
{
    public class AuthorService(IAuthorRepository repository) : IAuthorService
    {
        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            var authors = await repository.GetAllAsync();
            return authors.Select(a => a.ToDto());
        }

        public async Task<AuthorDto?> GetByIdAsync(int id)
        {
            var author = await repository.GetByIdAsync(id);
            return author?.ToDto();
        }

        public async Task CreateAsync(CreateAuthorDto dto)
        {
            if (await repository.ExistsByNameAsync(dto.Name))
            {
                throw new InvalidOperationException($"El autor '{dto.Name}' ya existe.");
            }

            var entity = dto.ToEntity();
            await repository.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdateAuthorDto dto)
        {
            if (await repository.ExistsByNameAsync(dto.Name, dto.Id))
            {
                throw new InvalidOperationException($"El nombre '{dto.Name}' ya está en uso por otro autor.");
            }

            var entity = await repository.GetByIdAsync(dto.Id)
                         ?? throw new KeyNotFoundException("Autor no encontrado.");

            entity.UpdateEntity(dto); // Mapeo de actualización
            await repository.UpdateAsync(entity);
        }

        public async Task DeactivateAsync(int id)
        {
            if (await repository.HasBooksAsync(id))
            {
                throw new InvalidOperationException("No se puede eliminar el autor porque tiene libros asociados.");
            }

            await repository.DeactivateAsync(id);
        }
    }
}
