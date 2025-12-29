using AzSmartLibrary.Core.Entities;

namespace AzSmartLibrary.Core.Interfaces
{
    public interface IAuthorRepository
    {
        
        Task<IEnumerable<Author>> GetAllAsync();

        Task<Author?> GetByIdAsync(int id);
       
        Task<bool> ExistsByNameAsync(string name);
        
        Task<bool> ExistsByNameAsync(string name, int currentId);

        Task AddAsync(Author author);

        Task UpdateAsync(Author author);
       
        Task DeactivateAsync(int id);
        
        Task<bool> HasBooksAsync(int authorId);
    }
}
