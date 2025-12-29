using AzSmartLibrary.Application.DTOs;

namespace AzSmartLibrary.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task<AuthorDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateAuthorDto dto);
        Task UpdateAsync(UpdateAuthorDto dto);
        Task DeactivateAsync(int id);
    }
}
