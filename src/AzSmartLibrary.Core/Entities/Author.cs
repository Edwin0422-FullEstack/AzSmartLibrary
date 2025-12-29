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
            set => field = value?.Trim() ?? throw new ArgumentNullException(nameof(value));
        }

        
        public virtual ICollection<Book> Books { get; set; } = [];
    }
}
