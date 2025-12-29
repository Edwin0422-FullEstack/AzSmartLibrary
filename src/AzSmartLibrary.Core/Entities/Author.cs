using AzSmartLibrary.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace AzSmartLibrary.Core.Entities
{
    public class Author : BaseEntity
    {
     
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public required string Name
        {
            get;
            // C# 14 Feature: 'field' keyword para lógica en setter sin backing field explícito
            set => field = value?.Trim() ?? throw new ArgumentNullException(nameof(value));
        }

        // Relación: Un autor tiene N libros.
        // Inicialización: Collection Expression [] (C# 12+)
        public virtual ICollection<Book> Books { get; set; } = [];
    }
}
