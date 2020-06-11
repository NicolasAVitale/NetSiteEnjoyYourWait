using System.Web;

namespace EnjoyYourWaitNetSite.Entities
{
    public class Producto
    {
        public int idProducto { get; set; }
        public string nombre { get; set; }
        public double precio { get; set; }
        public int idTipo { get; set; }
        public string imagen { get; set; }
        public int activo { get; set; }
    }
}
