using AzSmartLibrary.Core.Entities;

namespace AzSmartLibrary.Core.Interfaces
{
    public interface IBookRepository
    {
        // --- LECTURA ---

        /// <summary>
        /// Obtiene todos los libros activos.
        /// Útil para listados simples o APIs.
        /// </summary>
        Task<IEnumerable<Book>> GetAllAsync();

        /// <summary>
        /// REQUISITO CRÍTICO: Obtiene libros activos CON su Autor cargado.
        /// Vital para mostrar "Título - Nombre Autor" en la grilla principal sin hacer N+1 queries.
        /// </summary>
        Task<IEnumerable<Book>> GetAllWithAuthorsAsync();

        Task<Book?> GetByIdAsync(int id);

        /// <summary>
        /// Filtra libros por autor (útil si luego piden "Ver libros de García Márquez").
        /// </summary>
        Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId);

        // --- VALIDACIÓN DE NEGOCIO ---

        /// <summary>
        /// Verifica duplicados por título al crear.
        /// </summary>
        Task<bool> ExistsByTitleAsync(string title);

        /// <summary>
        /// Verifica duplicados por título al editar (excluyendo el actual).
        /// </summary>
        Task<bool> ExistsByTitleAsync(string title, int currentId);

        // --- ESCRITURA (Gestión de Estado) ---

        Task AddAsync(Book book);

        /// <summary>
        /// Actualiza título o cambia de autor.
        /// </summary>
        Task UpdateAsync(Book book);

        /// <summary>
        /// Soft Delete: Cambia IsActive a false.
        /// </summary>
        Task DeactivateAsync(int id);
    }
}
