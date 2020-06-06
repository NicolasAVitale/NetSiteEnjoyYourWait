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
        //[RegularExpression("^[0-9]+$", ErrorMessage = "Debe ingresar solo números")]
        //[MinLength(7, ErrorMessage = "El DNI debe tener 7 u 8 digitos")]
        //[MaxLength(8, ErrorMessage = "El DNI debe tener 7 u 8 digitos")]
        public string Nombre { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio.")]
        //[DataType(DataType.Currency)]
        //[RegularExpression("^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$", ErrorMessage = "Debe ingresar solo letras")]
        public double Precio { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es obligatorio.")]
        //[RegularExpression("^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$", ErrorMessage = "Debe ingresar solo letras")]
        public string Imagen { get; set; }
    }
}