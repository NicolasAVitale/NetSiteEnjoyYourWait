using com.sun.tools.javac.util;
using System;
using System.Collections.Generic;

namespace EnjoyYourWaitNetSite.Entities
{
    public class Promocion
    {
        public int idPromocion { get; set; }
        public string descripcion { get; set; }
        public string fechaInicio { get; set; }
        public string fechaBaja { get; set; }
        public int esPremio { get; set; }
        public int activo { get; set; }
    }
}
