using EnjoyYourWaitNetSite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnjoyYourWaitNetSite.Models
{
    public class PromocionProductoViewModel
    {
        public int IdPromocion { get; set; }
        public List<Producto> lstProducto { get; set; }
    }
}