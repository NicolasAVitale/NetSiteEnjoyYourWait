using EnjoyYourWaitNetSite.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EnjoyYourWaitNetSite.BusinessLogic
{
    public class BSFila : BSBase
    {
        public BSFila()
        {
        }

        public void EnviarCorreoConfirmacion(string email, string guid)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential("onthegrilleyw@gmail.com", "OTGrill2020");
            //smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;

            //Setting From , To and CC
            mail.From = new MailAddress("onthegrilleyw@gmail.com", "On The Grill");
            mail.CC.Add(new MailAddress(email));
            mail.Subject = "On The Grill - Confirmar ingreso a la fila";
            var builder = new StringBuilder();
            builder.Append("<head><h4>Confirmar ingreso a la fila</h4><hr /><p><font face='Calibri'>Hola, para confirmar tu ingreso a la fila debes hacer click en el siguiente Link. Si recibiste este correo por error, simplemente puedes borrarlo.</font></p></head><body><a href=\"" +
                ConfigurationManager.AppSettings.Get("ConfirmUrl") +
                "\" class=\"" + "button" + "\">Confirmar Ingreso</a></body><header> <hr /><p><font face='Calibri'>Muchas gracias, On The Grill.</font></p></header></html>");
            mail.Body = builder.ToString();
            smtpClient.Send(mail);
        }

        public async Task<bool> RegistrarTokenEmail(string guid, string email)
        {
            return await dataAccess.RegistrarTokenEmail(new GuidEmailRequest()
            {
                email =  email,
                guid = guid
            });
        }

        public async Task<Cliente> RegistrarCliente(Cliente cliente)
        {
            return await dataAccess.RegistrarCliente(cliente);
        }

        public async Task<bool> IngresarAFila(int idCliente, int cantComensales)
        {

            return await dataAccess.IngresarAFila(new FilaCliente()
            {
                idCliente = idCliente,
                cantComensales = cantComensales,
                esConfirmado = 0,
                activo = 1
            });
        }

        public async Task<EsperaResponse> CalcularTiempoYPersonasEsperaCliente(int idCliente, int capacidad, int tiempo)
        {
            return await dataAccess.CalcularTiempoYPersonasEsperaCliente(idCliente, capacidad, tiempo);
        }

        public async Task<EsperaResponse> CalcularTiempoYPersonasEsperaGeneral(int capacidad, int tiempo)
        {
            return await dataAccess.CalcularTiempoYPersonasEsperaGeneral(capacidad, tiempo);
        }

        public async Task<bool> ActualizarEstadoClientesEnRestaurante(int tiempo)
        {
            return await dataAccess.ActualizarEstadoClientesEnRestaurante(tiempo);
        }
    }
}
