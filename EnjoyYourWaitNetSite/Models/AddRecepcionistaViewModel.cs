using System;
using System.ComponentModel.DataAnnotations;

namespace EnjoyYourWaitNetSite.Models
{
    public class AddRecepcionistaViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio.")]
        public int Dni { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio.")]
        public string Nombre { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio.")]
        public string Apellido { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio.")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio.")]
        public DateTime FechaNacimiento { get; set; }
    }
}