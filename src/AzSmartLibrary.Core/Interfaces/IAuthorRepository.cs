using AzSmartLibrary.Core.Entities;

namespace AzSmartLibrary.Core.Interfaces
{
    public interface IAuthorRepository
    {
        // --- LECTURA ---
        // Solo debe traer autores activos (IsActive == true)
        Task<IEnumerable<Author>> GetAllAsync();

        Task<Author?> GetByIdAsync(int id);

        // --- VALIDACIÓN DE NEGOCIO (Soporte a Requisito) ---

        /// <summary>
        /// Verifica si ya existe un autor con el mismo nombre (Para creación).
        /// </summary>
        Task<bool> ExistsByNameAsync(string name);

        /// <summary>
        /// Sobrecarga vital para EDICIÓN.
        /// Verifica si el nombre existe en OTRO autor que no sea yo mismo.
        /// Evita que el sistema lance error si guardo mis propios cambios sin cambiar el nombre.
        /// </summary>
        /// <param name="name">Nombre a validar</param>
        /// <param name="currentId">ID del autor que se está editando</param>
        Task<bool> ExistsByNameAsync(string name, int currentId);

        // --- ESCRITURA (Modificaciones de Estado) ---

        Task AddAsync(Author author);

        /// <summary>
        /// Actualiza los datos del autor (Nombre).
        /// </summary>
        Task UpdateAsync(Author author);

        // --- SOFT DELETE (Requisito: No borrar, desactivar) ---

        /// <summary>
        /// Cambia el estado IsActive a false. 
        /// Mantiene la integridad referencial de los libros históricos.
        /// </summary>
        Task DeactivateAsync(int id);

        // --- INTEGRIDAD REFERENCIAL ---
        // Aún útil para saber si puedo desactivarlo sin afectar lógica de negocio activa
        Task<bool> HasBooksAsync(int authorId);
    }
}
