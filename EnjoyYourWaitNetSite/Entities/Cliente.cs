using System;

namespace EnjoyYourWaitNetSite.Entities
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public int dni { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string fechaNacimiento { get; set; }
        public int activo { get; set; }
    }
}
