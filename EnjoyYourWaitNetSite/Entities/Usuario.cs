using System;

namespace EnjoyYourWaitNetSite.Entities
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public int dni { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string fechaNacimiento { get; set; }
        public string contrasena { get; set; }
        public int idRol { get; set; }
        public int activo { get; set; }
    }
}
