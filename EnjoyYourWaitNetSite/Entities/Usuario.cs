using System;

namespace EnjoyYourWaitNetSite.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Contrasena { get; set; }
        public int IdRol { get; set; }
    }
}
