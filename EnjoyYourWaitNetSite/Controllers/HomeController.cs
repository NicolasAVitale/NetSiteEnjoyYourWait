using EnjoyYourWaitNetSite.BusinessLogic;
using EnjoyYourWaitNetSite.Helper;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class HomeController : Controller
    {
        private BSHome bsHome = new BSHome();
        public async Task<ActionResult> Index()
        {
            //if (SessionHelper.Cliente != null)
            //{
                //Ingresa al home de cliente y se cargan lo datos necesarios
            //} 
            //else
            //{
                //Al no estar un cliente en sesión se accede a la pantalla de home cliente sin cargar datos, 
                //habilitando la opción de login
            //}

            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            //smtpClient.Credentials = new System.Net.NetworkCredential("onthegrilleyw@gmail.com", "OTGrill2020");
            //// smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpClient.EnableSsl = true;
            //MailMessage mail = new MailMessage();

            ////Setting From , To and CC
            //mail.From = new MailAddress("onthegrilleyw@gmail.com", "On The Grill");
            //mail.To.Add(new MailAddress("jonysz95@hotmail.com"));
            //mail.CC.Add(new MailAddress("nicolas.a.vitale@gmail.com"));
            ////mail.Body = "Hola Joni y Nico";
            //smtpClient.Send(mail);
            string msg = await bsHome.GetHolaMundoAsync();
            ViewBag.Message = msg;
            return View();
        }

        public ActionResult IndexAdmin()
        {
            if (SessionHelper.Administrador != null)
            {
                //Ingresa al home de administrador y se cargan lo datos necesarios
            }
            else
            {
                //Al no estar un administrador en sesión se debe redirigir a la pantalla de login de administrador
            }
            return View();
        }

        public ActionResult IndexRecepcionista()
        {
            if (SessionHelper.Recepcionista != null)
            {
                //Ingresa al home de recepcionista y se cargan lo datos necesarios
            }
            else
            {
                //Al no estar un recepcionista en sesión se debe redirigir a la pantalla de login de rececpcionisa
            }
            return View();
        }
    }
}