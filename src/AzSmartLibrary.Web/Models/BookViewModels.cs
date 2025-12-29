using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AzSmartLibrary.Web.Models
{
    public class CreateBookViewModel
    {
        
        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "El título debe tener entre 2 y 200 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un autor")]
        [Display(Name = "Autor Principal")]
        public int? AuthorId { get; set; } 

    
        public SelectList? AuthorsList { get; set; }
    }
}
