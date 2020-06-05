using System.ComponentModel.DataAnnotations;

namespace EnjoyYourWaitNetSite.Models
{
    public class UserAuthenticationViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio.")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio.")]
        public string Contrasena { get; set; }
    }
}