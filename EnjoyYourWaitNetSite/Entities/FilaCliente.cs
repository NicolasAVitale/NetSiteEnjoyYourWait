using System;

namespace EnjoyYourWaitNetSite.Entities
{
    public class FilaCliente
    {
        public int idCliente { get; set; }
        public int cantComensales { get; set; }
        public DateTime fechaIngFila { get; set; }
        public DateTime fechaEgrFila { get; set; }
        public int esConfirmado { get; set; }
        public int activo { get; set; }
    }
}
