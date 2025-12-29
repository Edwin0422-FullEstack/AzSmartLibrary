using AzSmartLibrary.Application.DTOs;

namespace AzSmartLibrary.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<BookDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateBookDto dto);
        Task UpdateAsync(UpdateBookDto dto);
        Task DeactivateAsync(int id);
    }
}
