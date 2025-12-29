using System.ComponentModel.DataAnnotations;

namespace AzSmartLibrary.Web.Models
{
    public class CreateAuthorViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Display(Name = "Nombre Completo")]
        public string Name { get; set; } = string.Empty;
    }
}
