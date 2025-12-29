using AzSmartLibrary.Core.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzSmartLibrary.Core.Entities
{
    public class Book : BaseEntity
    {
        
        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200, MinimumLength = 2)]
        public required string Title { get; set; }

        
        [Required(ErrorMessage = "Debe seleccionar un autor")]
        public int AuthorId { get; set; }

        // Propiedad de Navegación
        // 'virtual' permite Lazy Loading si se requiere en el futuro
        [ForeignKey(nameof(AuthorId))]
        public virtual Author? Author { get; set; }
    }
}
