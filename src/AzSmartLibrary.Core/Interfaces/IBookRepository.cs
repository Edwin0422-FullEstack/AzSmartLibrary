using AzSmartLibrary.Core.Entities;

namespace AzSmartLibrary.Core.Interfaces
{
    public interface IBookRepository
    {
       
        Task<IEnumerable<Book>> GetAllAsync();

        Task<IEnumerable<Book>> GetAllWithAuthorsAsync();

        Task<Book?> GetByIdAsync(int id);

        Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId);

        Task<bool> ExistsByTitleAsync(string title);

        Task<bool> ExistsByTitleAsync(string title, int currentId);

        Task AddAsync(Book book);

        Task UpdateAsync(Book book);

        Task DeactivateAsync(int id);
    }
}
