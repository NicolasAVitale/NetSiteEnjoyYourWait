using EnjoyYourWaitNetSite.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnjoyYourWaitNetSite.Models
{
    public class UpdateProductoViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression("^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$", ErrorMessage = "Debe ingresar solo letras")]
        public string Nombre { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio.")]
        public double Precio { get; set; }
        public HttpPostedFileBase Imagen { get; set; }
    }
}