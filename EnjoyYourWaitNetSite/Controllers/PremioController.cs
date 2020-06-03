using EnjoyYourWaitNetSite.BusinessLogic;
using EnjoyYourWaitNetSite.Helper;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnjoyYourWaitNetSite.Controllers
{
    public class PremioController : Controller
    {
        private BSHome bsHome = new BSHome();
        public ActionResult Index()
        {
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