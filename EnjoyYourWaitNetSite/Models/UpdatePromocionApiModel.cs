using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnjoyYourWaitNetSite.Models
{
    public class UpdatePromocionApiModel
    {
        public string descripcion { get; set; }
        public string fechaInicio { get; set; }
        public string fechaBaja { get; set; }
        public int esPremio { get; set; }
    }
}